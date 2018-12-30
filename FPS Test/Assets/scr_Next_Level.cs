using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_Next_Level : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<scr_Player_Movement>() != null)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }

}
