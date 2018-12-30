using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Health_Pack : MonoBehaviour {

    public AudioSource sound;

    float playerHealth;
    
    void OnTriggerEnter(Collider other)
    {

        playerHealth = FindObjectOfType<scr_Player_Health>().health;

        if (playerHealth < 100 && other.GetComponent<scr_Player_Movement>() != null)
        {

            sound.Play();

            FindObjectOfType<scr_Player_Health>().AddHealth(25);

            Destroy(this.gameObject, 0f);

        }

    }

}
