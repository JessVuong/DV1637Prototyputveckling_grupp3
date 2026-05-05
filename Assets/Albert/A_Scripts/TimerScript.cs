using JetBrains.Annotations;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    bool isRunning = false;
    public float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) 
            return;

        remainingTime -= Time.deltaTime;
        
        if (remainingTime <= 0 )
        {
            remainingTime = 0;
            gameObject.GetComponent<GameManagerScript>().Defeat();
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
