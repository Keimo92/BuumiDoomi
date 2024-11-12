using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickupable
{
    public AmmoData ammoData;

    public override void OnPickup(Entity entity)
    {
        if(entity.entityType is Entity.EntityMask.Player)
        {
            PlayerWeapon weapon = entity.GetComponent<PlayerWeapon>();
       
            if ( weapon.ammoCount < weapon.maxAmmo )
            {
                weapon.AddAmmmo(ammoData.ammoCount);  
                base.OnPickup(entity);
                Destroy(gameObject);
            }
        }

    }
}