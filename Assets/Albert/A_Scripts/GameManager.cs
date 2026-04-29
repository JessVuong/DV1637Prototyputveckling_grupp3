using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public TimerScript timer;
    public PauseMenu pauseMenu;
    public GameObject hud;
    public GameObject defeatScreen;
    public GameObject victoryScreen;
    public bool gameIsPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the Escape key is pressed and if it is it pauses or resumes the game depending on if it is currently paused
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                pauseMenu.Resume();
            }
            else
            {
                pauseMenu.Pause();
            }
        }
    }

    public void Defeat()
    {
        timer.StopTimer();
        hud.SetActive(false);
        defeatScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Victory()
    {
        timer.StopTimer();
        hud.SetActive(false);
        victoryScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        float value = timer.remainingTime;

        int minutes = Mathf.FloorToInt(value / 60f);
        int seconds = Mathf.FloorToInt(value - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);
        victoryScreen.GetComponentInChildren<TextMeshProUGUI>().text = "You managed to escape with " + time + " to go!";
    }
}
