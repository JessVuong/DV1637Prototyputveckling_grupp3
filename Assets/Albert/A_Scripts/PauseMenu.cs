using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameManagerScript game;

    public void Resume()
    {
        //Resumes the game
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        game.gameIsPaused = false;
    }

    public void Pause()
    {
        //Pauses the game
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        game.gameIsPaused = true;
    }

    public void MainMenu()
    {
        //Loads the scene MainMenu if MainMenu is in the SceneList
        Time.timeScale = 1f;
        game.gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }
}
