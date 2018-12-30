using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Player_Health : MonoBehaviour {

    public float health = 100;

    public Text healthText;

    public GameObject manager;

    public Animator animator;

    private void OnEnable()
    {

        animator.SetBool("DamageTaken", false);

    }

    public void TakeDamage (float damage)
    {

        health -= damage;

        StartCoroutine(DamageEffect());

        if(health <= 0)
        {

            manager.GetComponent<scr_Game_Manager>().EndGame();

        }

    }

    public void AddHealth (float healthToAdd)
    {

        if(health + healthToAdd <= 100)
        {

            health += healthToAdd;

        }

        else
        {

            health = 100;

        }

    }

    void Update()
    {

        healthText.text = health.ToString();

    }

    IEnumerator DamageEffect()
    {

        animator.SetBool("DamageTaken", true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("DamageTaken", false);

    }

}
