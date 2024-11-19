using UnityEngine;

public class Button : Interactable
{
    public override void Interact(Entity entity)
    {
        if ( entity.entityType == Entity.EntityMask.Player && !Interacted )
        {
            Interacted = true;
            base.Interact(entity);
        }

        else if (Interacted)
        {
            base.HasbeenInteracted(entity);
        }
    }
}