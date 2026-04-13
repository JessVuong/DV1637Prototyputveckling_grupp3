using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public SceneAsset mainMenuScene;
    public GameObject pauseMenu;
    public GameManagerScript game;

    public void Resume()
    {
        //Resumes the game
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        game.gameIsPaused = false;
    }

    public void Pause()
    {
        //Pauses the game
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        game.gameIsPaused = true;
    }

    public void MainMenu()
    {
        //Loads the scene MainMenu if MainMenu is in the SceneList
        Time.timeScale = 1f;
        game.gameIsPaused = false;
        SceneManager.LoadScene(mainMenuScene.name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }
}
