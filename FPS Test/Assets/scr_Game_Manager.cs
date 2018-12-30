using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_Game_Manager : MonoBehaviour {

    bool gameHasEnded = false;

    public void EndGame()
    {

        if(gameHasEnded == false)
        {

            gameHasEnded = true;

            Debug.Log("Game Over");

            RestartGame();

        }

    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
