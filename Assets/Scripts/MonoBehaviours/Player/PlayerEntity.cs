using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    ScreenFlash screenFlash;
    public bool isAlive;

    [Header("Screen Flash Colors")]
    [SerializeField] private float onDeathFadeDuration;
    [SerializeField] private float onDamageFlashDuration;
    [SerializeField] private Color onDeathFadeColor;
    [SerializeField] private Color onDamageFlashColor;

    //Testing purposes only. Will remove this after review.
    public float resetSceneTime;
    public override void Kill()
    {
        isAlive = false;
        screenFlash.FadeToColor(onDeathFadeColor,onDeathFadeDuration);
        GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void Start()
    {
        isAlive = true;
        screenFlash = FindAnyObjectByType<ScreenFlash>();
        base.GetHealth();
    }

    public override void Damage(float damage)
    {
        if ( isAlive )
        {
            screenFlash.FlashColor(onDamageFlashColor, onDamageFlashDuration);
            base.Damage(damage);
        }
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged; // Subscribe to the event
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged; // Unsubscribe from the event
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        // Respond to the game state change, for example:
        Debug.Log("Game State Changed to: " + newState);

        // Update the UI or other elements based on the new game state
    }
}
