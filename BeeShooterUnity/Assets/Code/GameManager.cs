using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameWon = false;
    public PlayerShip PlayerShip;
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        isGameWon = false;
    }
    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1);
        LevelManager.instance.GameOver();
    }

    public void winGame()
    {
        if (isGameWon == false)
        {
            LevelManager.instance.gameDone();
            isGameWon = true;
            PlayerShip.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        

    }
}
