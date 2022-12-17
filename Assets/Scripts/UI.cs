using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("Screen:")]
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pauseScreen;

    private GameObject currentScreen;

    private void Awake()
    {
        currentScreen = gameScreen;
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        currentScreen = loseScreen;
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        currentScreen = pauseScreen;
    }

    public void Resume()
    {
        loseScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        currentScreen = gameScreen;
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
