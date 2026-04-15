using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    public TimerScript timer;
    public GameObject timerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = timer.remainingTime;

        int minutes = Mathf.FloorToInt(value / 60f);
        int seconds = Mathf.FloorToInt(value - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerUI.GetComponent<TextMeshProUGUI>().text = time;
    }
}
