using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;
using System;

public class HealthPickUp : Pickupable
{
    public HealthPackData healthPack;
    public override void OnPickup(Entity entity)
    {
        if(entity.entityType == Entity.EntityMask.Player)
        {
            if ( entity.GetHealth() < entity.GetMaxHealth() )
            {
                PlayerAnimationManager.ChangeFaceState(PlayerAnimationManager.PlayerFaceState.PickUp);
                base.OnPickup(entity);
                entity.AddHealth(healthPack.healingAmount);
                PlayerAnimationManager.ChangeFaceState(PlayerAnimationManager.PlayerFaceState.Idle);
                Destroy(gameObject);
            }
        }

    }
}