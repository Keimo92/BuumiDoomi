using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private Color onLevelExitColor;
    [SerializeField] private float onLevelExitFadeDuration;
    ScreenFlash screenFlash;
    private void Start()
    {
        screenFlash = FindFirstObjectByType<ScreenFlash>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                screenFlash.FadeToColor(onLevelExitColor, onLevelExitFadeDuration);
                GameManager.Instance.SetGameState(GameManager.GameState.LoadNextLevel);
            }
        }
    }
}

