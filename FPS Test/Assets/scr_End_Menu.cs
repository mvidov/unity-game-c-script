using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_End_Menu : MonoBehaviour {

	void Start () {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void LoadMenu()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Main Menu");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Load Menu");

    }

    public void QuitGame()
    {

        Application.Quit();

        Debug.Log("Quit Game");

    }

}
