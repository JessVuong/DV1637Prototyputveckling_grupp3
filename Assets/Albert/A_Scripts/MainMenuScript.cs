using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //The scene that loads when you press "Start Game"
    public SceneAsset startGameScene;

    public void StartGame()
    {
        //Loads the scene MainMenu if MainMenu is in the SceneList
        SceneManager.LoadScene(startGameScene.name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }
}
