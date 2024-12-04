using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;
using System;

public class HealthPickUp : Pickupable
{
    [Header("Data")]
    public HealthPackData healthPack;

    public override void OnPickup(Entity entity)
    {
        if(entity.entityType == Entity.EntityMask.Player)
        {
            if ( entity.GetCurrentHealth() < entity.GetMaxHealth() )
            {
                base.OnPickup(entity);
                entity.AddCurrentHealth(healthPack.healingAmount);
                Destroy(gameObject);
            }
        }

    }
}