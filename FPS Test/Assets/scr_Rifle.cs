using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Rifle : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 5f;
    public float impactForce = 100f;

    public int maxAmmo = 20;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public int reserveAmmo = 0;
    private int ammoDifference;
    private bool canShoot = true;

    public Animator animator;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Light muzzleLight;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Text currentAmmoText;
    public Text reserveAmmoText;

    public AudioSource fireSound;
    public AudioSource reloadSound;
    public AudioSource shellSound;

    void Start()
    {

        currentAmmo = 0;

    }

    void OnEnable()
    {

        isReloading = false;

        animator.SetBool("Reloading", false);

        currentAmmoText.text = currentAmmo.ToString();
        reserveAmmoText.text = reserveAmmo.ToString();

        nextTimeToFire = Time.time + 0.5f;

    }

	void Update () {

        currentAmmoText.text = currentAmmo.ToString();
        reserveAmmoText.text = reserveAmmo.ToString();

        if (isReloading)
        {

            return;

        }

        if(currentAmmo <= 0 && reserveAmmo > 0)
        {

            StartCoroutine(Reload());

            return;

        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reserveAmmo > 0)
        {

            StartCoroutine(Reload());

            return;

        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0 && canShoot == true && scr_Pause_Menu.isPaused == false)
        {

            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();

        }

        currentAmmoText.text = currentAmmo.ToString();
        reserveAmmoText.text = reserveAmmo.ToString();

    }

    IEnumerator Reload()
    {

        canShoot = false;

        yield return new WaitForSeconds(0.25f);

        reloadSound.Play();

        isReloading = true;

        animator.SetBool("Reloading", true);

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);

        isReloading = false;

        ammoDifference = maxAmmo - currentAmmo;

        if(reserveAmmo >= ammoDifference)
        {

            currentAmmo += ammoDifference;
            reserveAmmo -= ammoDifference;

        }

        else
        {

            currentAmmo += reserveAmmo;
            reserveAmmo = 0;

        }

        Invoke("EnableShooting", 0.3f);

    }

    void EnableShooting()
    {

        canShoot = true;

    }

    void Shoot()
    {

        fireSound.Play();
        Invoke("ShellFalling", 0.1f);

        muzzleFlash.Play();
        muzzleLight.enabled = true;

        Invoke("TurnOffLight", 0.05f);

        currentAmmo--;

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);

            scr_Target target = hit.transform.GetComponent<scr_Target>();

            if(target != null)
            {

                target.TakeDamage(damage);

            }

            if(hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            impactGO.GetComponent<ParticleSystem>().Play();

            Destroy(impactGO, 2f);

        }

    }

    void TurnOffLight()
    {

        muzzleLight.enabled = false;

    }

    void ShellFalling()
    {

        shellSound.Play();

    }

}
