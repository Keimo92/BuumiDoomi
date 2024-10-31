using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    ScreenFlash screenFlash;
    public bool IsAlive;
    
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
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
