using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
