using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;

public class HealthPickUp : Pickupable
{
    public HealthPackData healthPack;

    public override void OnPickup(Entity entity)
    {
        base.OnPickup(entity);
        entity.AddHealth(healthPack.healingAmount);
        Destroy(gameObject, 0.3f);
    }
}