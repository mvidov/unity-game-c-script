using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Zombie_Follow_test : MonoBehaviour {

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject enemy;

    public float targetDistance;
    public float activeRange = 10f;

    public float enemySpeed;
    public float enemySpeedMax = 0.01f;

    private bool isAttacking = false;

    private bool isDying = false;

    public RaycastHit shot;

    //public Quaternion rotation = Quaternion.identity;

    //public Vector3 eulerRotation;

    float lockPos = 0;

    //[SerializeField] public Vector3 position;
    //[SerializeField] public Transform playerPos;
    //[SerializeField] public Transform lookTarget = null;

    void Update()
    {

        //lookTarget.SetPositionAndRotation(playerPos.position, playerPos.rotation);
        //position.Set(player.transform.position.x, 2f, player.transform.position.z);
        //lookTarget.SetPositionAndRotation(position, player.transform.rotation);

        //transform.LookAt(lookTarget);

        //enemy.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        //eulerRotation.Set(0, player.transform.rotation.y - 180, 0);

        //rotation.eulerAngles = new Vector3(player.transform.rotation.x, player.transform.rotation.y - 180, 0);

        //rotation.eulerAngles = eulerRotation;

        //transform.SetPositionAndRotation(transform.position, rotation);

        if(isDying == false)
        {

            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);

            transform.LookAt(player.transform);

            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);

        }

        //Mathf.Clamp(transform.rotation.x, -5f, 5f);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot) && isDying == false && scr_Pause_Menu.isPaused == false)
        {

            targetDistance = shot.distance;

            if (targetDistance < activeRange)
            {

                enemySpeed = enemySpeedMax;

                if (isAttacking == false)
                {

                    enemy.GetComponent<Animation>().Play("Walking");

                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);

                }

                if (targetDistance < 1f && isAttacking == false && Mathf.Abs(player.transform.position.y - transform.position.y) < 0.5 && shot.transform.tag == "PlayerTag")
                {

                    StartCoroutine(Attack());

                }

            }

            else
            {

                enemySpeed = 0f;

                enemy.GetComponent<Animation>().Play("Idle");

            }

        }

    }

    IEnumerator Attack()
    {

        enemySpeed = 0;

        isAttacking = true;

        enemy.GetComponent<Animation>().Play("Attacking");

        yield return new WaitForSeconds(1f);

        if(targetDistance < 1f && isDying == false)
        {

            FindObjectOfType<scr_Player_Health>().TakeDamage(20);

        }

        yield return new WaitForSeconds(1f);

        isAttacking = false;

    }

    public void Die()
    {

        isDying = true;

        StartCoroutine(DieAnim());

    }

    IEnumerator DieAnim()
    {

        enemy.GetComponent<Animation>().Play("Dying");

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject, 0f);

    }

}
