using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    /* Start the game in a new scene */
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    /*  Quit the application */
    public void QuitGame()
    {
        Application.Quit();
    }
}
