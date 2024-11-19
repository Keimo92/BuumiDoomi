using UnityEngine;

public class Button : Interactable
{

    public override void Interact(Entity entity)
    {
        if ( entity.entityType == Entity.EntityMask.Player )
        {
            base.Interact(entity);
        }
    }
}