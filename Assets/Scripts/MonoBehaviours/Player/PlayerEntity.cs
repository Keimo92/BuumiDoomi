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
}

