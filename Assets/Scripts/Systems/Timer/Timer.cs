using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float startTime;

    PlayerEntity player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
        ResetTimer();
    }

    private void Update()
    {
        if ( player.isAlive)
        {
            RunTimer();
        }

        if ( !player.isAlive ) //After checkpoint system we need to fix this because IsAlive is going to be false after player dies. We dont want to reset the time when respawn from checkpoint :)
        {
            ResetTimer();
        }
    }
    private void RunTimer()
    {
        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetTimer()
    {
        startTime = Time.time;
        timerText.text = "00:00"; 
    }
}
