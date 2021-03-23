using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    float delayInSeconds = 2f;
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start Game");
        ResetGame();
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
