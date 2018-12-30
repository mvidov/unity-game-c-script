using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Target : MonoBehaviour {

    public float health = 50f;

    public GameObject zombie;

    public void TakeDamage(float damage)
    {

        health -= damage;

        if (health <= 0f)
        {

            if(zombie != null)
            {

                zombie.GetComponent<scr_Zombie_Follow_test>().Die();

            }

            else
            {

                Die();

            }

        }

    }

    void Die()
    {

        Destroy(gameObject);

    }

}
