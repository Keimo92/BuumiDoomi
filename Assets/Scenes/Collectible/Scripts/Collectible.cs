using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Pickupable
{
    public override void OnPickup(Entity entity)
    {
        if ( entity.entityType == Entity.EntityMask.Player )
        {
            CollectibleDataPersistence.CollectiblesLeft--;

            Destroy(gameObject);

            base.OnPickup(entity);
        }
    }
}