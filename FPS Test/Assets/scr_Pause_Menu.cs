using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_Pause_Menu : MonoBehaviour {

    public static bool isPaused = false;

    public GameObject pauseMenuUI;

	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {

                Resume();

            }

            else
            {

                Pause();

            }

        }

	}

    void Pause()
    {

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;


    }

    public void Resume()
    {

        pauseMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        isPaused = false;

    }

    public void LoadMenu()
    {

        Resume();

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
