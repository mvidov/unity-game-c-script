using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Zombie_Follow : MonoBehaviour {

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject enemy;

    public float targetDistance;
    public float activeRange = 10f;

    public float enemySpeed;
    public float enemySpeedMax = 0.01f;

    public int attackTrigger;
    private bool isAttacking = false;

    public RaycastHit shot;

    void Update()
    {

        transform.LookAt(player.transform);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {

            targetDistance = shot.distance;

            if(targetDistance < activeRange)
            {

                enemySpeed = enemySpeedMax;

                if (attackTrigger == 0)
                {

                    enemy.GetComponent<Animation>().Play("Walking");

                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);

                }

                if(targetDistance < 1f && isAttacking == false)
                {

                    attackTrigger = 1;

                }

                else if(targetDistance > 1f)
                {

                    attackTrigger = 0;

                }

            }

            else
            {

                enemySpeed = 0f;

                enemy.GetComponent<Animation>().Play("Idle");

            }

        }

        if(attackTrigger == 1)
        {

            enemySpeed = 0;

            StartCoroutine(Attack());

        }

    }

    IEnumerator Attack()
    {

        isAttacking = true;

        enemy.GetComponent<Animation>().Play("Attacking");

        yield return new WaitForSeconds(2f);

        isAttacking = false;

    }

}
