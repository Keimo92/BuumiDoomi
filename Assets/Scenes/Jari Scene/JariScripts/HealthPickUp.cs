using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public HealthPack HealthPack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            entity.AddHealth(HealthPack.HealingAmount);
        }
        Destroy(gameObject);
    }
}