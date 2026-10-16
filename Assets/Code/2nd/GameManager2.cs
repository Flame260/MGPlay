using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public GameObject gameOverUI;
    public GameObject gameClearUI;

    bool ended;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (gameClearUI != null) gameClearUI.SetActive(false);

    }

    public void GameOver()
    {
        if (ended) return;
        ended = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        if (ended) return;
        ended = true;
        gameClearUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}

