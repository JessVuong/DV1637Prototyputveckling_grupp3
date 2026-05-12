using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameManagerScript game;
    public TimerScript timer;


    [Header ("Settings")]
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private CameraController gameplayCamera;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject sliderSensitivity;
    [SerializeField] private TextMeshProUGUI valueSensitivity;
    [SerializeField] private float sensitivityMultiplier = 4f;
    [SerializeField] private GameObject sliderAudio;
    [SerializeField] private TextMeshProUGUI valueAudio;

    public void Resume()
    {
        //Resumes the game
        timer.StartTimer();
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        game.gameIsPaused = false;
    }

    public void Pause()
    {
        //Pauses the game
        timer.StopTimer();
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        game.gameIsPaused = true;
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

        // Set the sliders and labels to the current values
        sliderSensitivity.GetComponent<Slider>().SetValueWithoutNotify(gameplayCamera.mSensitivity / sensitivityMultiplier);
        sliderAudio.GetComponent<Slider>().SetValueWithoutNotify(audioSource.volume * 100f);
        valueSensitivity.text = (gameplayCamera.mSensitivity / sensitivityMultiplier).ToString() + "%";
        valueAudio.text = (audioSource.volume * 100f).ToString() + "%";
    }

    public void SetSliderValues()
    {
        gameplayCamera.mSensitivity = sliderSensitivity.GetComponent<Slider>().value * sensitivityMultiplier;
        audioSource.volume = sliderAudio.GetComponent<Slider>().value / 100f;
        valueSensitivity.text = (gameplayCamera.mSensitivity / sensitivityMultiplier).ToString() + "%";
        valueAudio.text = sliderAudio.GetComponent<Slider>().value.ToString() + "%";
    }

    public void Return()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
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
