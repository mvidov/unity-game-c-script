using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Door_Console : MonoBehaviour {

    bool isActivated = false;

    bool playerInRange = false;

    public Animator doorAnimator;

    public Light consoleLight;

    public AudioSource bleep;

    Collider[] array;

    void Update()
    {

        array = Physics.OverlapSphere(this.transform.position, 1f);

        foreach(Collider cl in array)
        {

            if(cl.transform.tag == "PlayerTag")
            {

                playerInRange = true;

            }

        }

        if (Input.GetKeyDown(KeyCode.E) && isActivated == false && playerInRange == true)
        {

            Debug.Log("if statement passed");

            StartCoroutine(openDoor());

            isActivated = true;

        }

    }

    IEnumerator openDoor()
    {

        bleep.Play();

        consoleLight.enabled = true;

        Debug.Log("Door Opening");

        doorAnimator.SetBool("Opened", true);

        yield return new WaitForSeconds(0.5f);

        doorAnimator.SetBool("Finished Opening", true);

    }

}
