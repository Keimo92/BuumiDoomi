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
    private float elapsedTime;
    private TimerState timerState = TimerState.Running;

    PlayerEntity player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
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
        elapsedTime = Time.time - GameManager.Instance.gameStartTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
