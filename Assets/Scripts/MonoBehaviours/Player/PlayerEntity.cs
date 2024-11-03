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
        StartCoroutine(ResetLevel());
    }

    private void Start()
    {
        isAlive = true;
        screenFlash = FindAnyObjectByType<ScreenFlash>();
        base.GetHealth();
    }

    // This just testing purposes only. TODO: GameManager which handles these
    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(resetSceneTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
