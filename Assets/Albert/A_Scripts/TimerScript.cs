using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] float remainingTime;
    bool isRunning = false;
    public GameManagerScript game;

    private void Start()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) 
            return;

        remainingTime -= Time.deltaTime;
        
        if (remainingTime <= 0 )
        {
            game.Defeat();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
