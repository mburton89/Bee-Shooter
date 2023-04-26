using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuShit : MonoBehaviour
{
    public string TheGame;
    public string Credits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(TheGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        //SceneManager.LoadScene(Credits); MAKE ACTIVE ONCE WE HAVE A CREDITS SCENE
    }
}
