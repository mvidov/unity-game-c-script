using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Damager : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {

        FindObjectOfType<scr_Player_Health>().TakeDamage(20);

        Destroy(this.gameObject, 0f);

    }

}
