using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(3);
<<<<<<< Updated upstream
        SceneManager.LoadScene(0);

=======
        LevelManager.instance.GameOver();
    }

    public void winGame()
    {
        LevelManager.instance.gameDone();
>>>>>>> Stashed changes
    }
}
