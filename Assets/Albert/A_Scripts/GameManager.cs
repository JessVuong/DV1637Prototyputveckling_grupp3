using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public TimerScript timer;
    public PauseMenu pauseMenu;
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
                timer.StartTimer();
            }
            else
            {
                pauseMenu.Pause();
                timer.StartTimer();
            }
        }
    }

    public void Defeat()
    {
        print("lose");
        Time.timeScale = 0f;
        timer.StopTimer();
    }
}
