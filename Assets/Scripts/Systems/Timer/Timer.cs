using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    private enum TimerState
    {
        Running,
        Stopped
    }

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerResetTime = 5;
    private float startTime;
    private float elapsedTime;
    private TimerState timerState = TimerState.Running;

    PlayerEntity player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
        ResetTimer();
    }

    private void Update()
    {
        if(timerState is TimerState.Running)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetTimer()
    {
        timerState = TimerState.Running;
        startTime = Time.time;
        elapsedTime = 0;
        timerText.text = "00:00";
    }

    public void ShowFinishedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format($"Time Taken: {minutes:00}:{seconds:00}");
    }

    public void OnLevelFinished(Component sender, object data)
    {
        timerState = TimerState.Stopped;
        ShowFinishedTime();
    }
}
