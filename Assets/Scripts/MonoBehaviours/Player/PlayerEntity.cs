using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerEntity : Entity
{
    ScreenFlash screenFlash;
    public bool isAlive;
    
    [Header("Screen Flash Colors")]
    [SerializeField] private float onDeathFadeDuration;
    [SerializeField] private float onDamageFlashDuration;
    [SerializeField] private Color onDeathFadeColor;
    [SerializeField] private Color onDamageFlashColor;

    [Header("Events")]
    [SerializeField] private GameEvent onPlayerTakeDamageEvent;

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
        base.GetMaxHealth();
    }

    public override void Damage(float damage)
    {
        if ( isAlive )
        {
            onPlayerTakeDamageEvent.Raise(this, null);
            screenFlash.FlashColor(onDamageFlashColor, onDamageFlashDuration);
            base.Damage(damage);
        }
    }
}

