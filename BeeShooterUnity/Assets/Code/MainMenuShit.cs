using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuShit : MonoBehaviour
{
    public string TheGame;
    public string Credits;

    public string Menu;

    public GameObject creditsScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(TheGame);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(Menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        creditsScreen.transform.localScale = Vector3.one;
    }


    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
