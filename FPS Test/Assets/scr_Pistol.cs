using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Pistol : MonoBehaviour {

    public float damage = 5f;
    public float range = 50f;
    public float fireRate = 2f;
    public float impactForce = 50f;

    public int maxAmmo = 6;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool canShoot = true;

    public Animator animator;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Light muzzleLight;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Text currentAmmoText;
    public Text reserveAmmoText;

    public AudioSource shootSound;
    public AudioSource reloadSound;

    void Start()
    {

        currentAmmo = maxAmmo;

    }

    void OnEnable()
    {

        isReloading = false;

        animator.SetBool("Reloading", false);

        currentAmmoText.text = currentAmmo.ToString();
        reserveAmmoText.text = "∞";

        nextTimeToFire = Time.time + 0.5f;

    }

    void Update()
    {

        if (isReloading)
        {

            return;

        }

        if (currentAmmo <= 0)
        {

            StartCoroutine(Reload());

            return;

        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {

            StartCoroutine(Reload());

            return;

        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && canShoot == true && scr_Pause_Menu.isPaused == false)
        {

            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();

        }

        currentAmmoText.text = currentAmmo.ToString();

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

        currentAmmo = maxAmmo;

        isReloading = false;

        Invoke("EnableShooting", 0.3f);

    }

    void EnableShooting()
    {

        canShoot = true;

    }

    void Shoot()
    {

        shootSound.Play();

        muzzleFlash.Play();
        muzzleLight.enabled = true;

        Invoke("TurnOffLight", 0.05f);

        currentAmmo--;

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);

            scr_Target target = hit.transform.GetComponent<scr_Target>();

            if (target != null)
            {

                target.TakeDamage(damage);

            }

            if (hit.rigidbody != null)
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

}
