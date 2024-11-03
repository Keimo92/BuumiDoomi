using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickupable
{
    public AmmoData ammoData;

    public override void OnPickup(Entity entity)
    {
        PlayerWeapon weapon = entity.GetComponent<PlayerWeapon>();
        weapon.AmmoCount += ammoData.ammoCount;
        base.OnPickup(entity);
        Destroy(gameObject, 0.3f);
    }
}
