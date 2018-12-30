using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Rifle_Ammo_Pickup : MonoBehaviour {

    public AudioSource ammoSound;

    public GameObject rifle;

    void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<scr_Player_Movement>() != null)
        {

            ammoSound.Play();

            rifle.GetComponent<scr_Rifle>().reserveAmmo += 20;

            //FindObjectOfType<scr_Rifle>().reserveAmmo += 20;

            Destroy(this.gameObject, 0f);

        }

    }

}
