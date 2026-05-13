using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public SoundManager soundManager;

    private void Awake()
    {
        SoundManager.PlaySound(SoundType.BackgroundMusic,1,true);
    }

    public void StartGame()
    {
        //Loads the scene MainMenu if MainMenu is in the SceneList
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }
}
