using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerResetTime = 5;
    private float startTime;
    private float elapsedTime;

    PlayerEntity player;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        player = FindAnyObjectByType<PlayerEntity>();
        ResetTimer();
    }

    private void Update()
    {
        if ( player.isAlive && !gameManager.onLevelFinished )
        {
            RunTimer();
        }
        if ( gameManager.onLevelFinished )
        {
            LevelFinishedTime();
            StartCoroutine(OnLevelFinishedRoutine());
        }
    }

    private void RunTimer()
    {
        elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetTimer()
    {
        startTime = Time.time;
        elapsedTime = 0;
        timerText.text = "00:00";
    }

    public void LevelFinishedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format($"Time Taken: {minutes:00}:{seconds:00}");
    }

    private IEnumerator OnLevelFinishedRoutine()
    {
        yield return new WaitForSeconds(timerResetTime);
        ResetTimer();
    }
}
