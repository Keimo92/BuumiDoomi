using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Pickupable
{

    public static event Action<Collectible> OnCollectiblePickedUp;

    public override void OnPickup(Entity entity)
    {
        if ( entity.entityType == Entity.EntityMask.Player )
        {
            OnCollectiblePickedUp?.Invoke(this);

            Destroy(gameObject);
        }
    }
}