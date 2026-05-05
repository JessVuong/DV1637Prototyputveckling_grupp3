using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    [Tooltip("Timer variables")]
    public TimerScript timer;
    public GameObject timerUI;
    private float startTime;

    [Tooltip("Hint variables")]
    [SerializeField] private GameObject hint;
    private float hintDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = timer.remainingTime;
    }

    // Update is called once per frame
    void Update()
    {
        float value = timer.remainingTime;

        int minutes = Mathf.FloorToInt(value / 60f);
        int seconds = Mathf.FloorToInt(value - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerUI.GetComponent<TextMeshProUGUI>().text = time;

        if (timer.remainingTime <= startTime / 2 && timerUI.GetComponent<TextMeshProUGUI>().color != Color.red)
        {
            timerUI.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (timer.remainingTime <= startTime / 4 && timerUI.GetComponent<Animator>().enabled == false)
        {
            timerUI.GetComponent<Animator>().enabled = true;
        }
    }

    public void ShowHint(string text, float duration = 3f)
    {
        hint.GetComponent<TextMeshProUGUI>().text = text;
        hintDuration = duration;
        StartCoroutine("WaitForDuration");
    }

    public IEnumerator WaitForDuration()
    {
        hint.SetActive(true);
        yield return new WaitForSeconds(hintDuration);
        hint.SetActive(false);
    }
}
