using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] players;

    public bool isPaused;
    public GameObject pausePanel;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if(isPaused)
        {
            AudioManager.instance.PlaySFX(5);
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            AudioManager.instance.PlaySFX(4);
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        AudioManager.instance.PlaySFX(0);
        PauseUnpause();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySFX(0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach(GameObject player in players)
        {
            if(player.activeSelf)
            {
                aliveCount++;
            }
        }

        if(aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        AudioManager.instance.PlaySFX(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
