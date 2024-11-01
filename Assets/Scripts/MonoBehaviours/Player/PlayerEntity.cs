using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    ScreenFlash screenFlash;
    public bool IsAlive;

    //Testing purposes only. Will remove this after review.
    public float resetTime;
    
    public override void Kill()
    {
       IsAlive = false;
       screenFlash.FadeToBlack();
       StartCoroutine(ResetLevel());
    }

    private void Start()
    {
        IsAlive = true;
        screenFlash = FindAnyObjectByType<ScreenFlash>();
        base.GetHealth();
    }

    // This just testing purposes only.
    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(resetTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
