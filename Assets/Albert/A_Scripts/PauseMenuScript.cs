using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public SceneAsset mainMenuScene;
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    public void Update()
    {
        //Checks if the Escape key is pressed and if it is it pauses or resumes the game depending on if it is currently paused
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Resumes the game
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //Pauses the game
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        //Loads the scene MainMenu if MainMenu is in the SceneList
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(mainMenuScene.name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }
}
