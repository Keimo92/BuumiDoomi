using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelTrigger : MonoBehaviour
{
 
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                GameManager.Instance.SetGameState(GameManager.GameState.LoadNextLevel);
            }
        }
    }
}

