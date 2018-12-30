using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sniper_Ammo_Pickup : MonoBehaviour {

    public AudioSource ammoSound;

    public GameObject sniper;

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<scr_Player_Movement>() != null)
        {

            ammoSound.Play();

            sniper.GetComponent<scr_Sniper>().reserveAmmo += 5;

            Destroy(this.gameObject, 0f);

        }

    }

}
