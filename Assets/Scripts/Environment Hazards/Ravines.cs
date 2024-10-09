using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ravines : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            entity.Kill();
        }

        if ( other.gameObject.CompareTag("Legs") )
        {
            Destroy(gameObject);
        }
    }
}
