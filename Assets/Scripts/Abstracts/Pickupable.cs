using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Pickupable : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private ScreenFlash screenFlashComponent;

    [Header("Settings")]
    [SerializeField] bool screenFlash;
    [SerializeField] Color screenFlashColor;
    [SerializeField] float screenFlashDuration;

    [SerializeField] bool pickupDialogue;
    [SerializeField] DialogueData onPickupDialogueData;

    [Header("Events")]
    [SerializeField] GameEvent onPickupablePickedUpEvent;


    private void Start()
    {
        //Find the dialogue manager and screenflash components from the scene. This could be refactored later.
        dialogueManager = FindAnyObjectByType<DialogueManager>();
        screenFlashComponent = FindAnyObjectByType<ScreenFlash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player walks on this object -> Do pickup logic
        if(other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entity.entityType == Entity.EntityMask.Player )
            {
                OnPickup(entity);
            }
        }
    }

    public virtual void OnPickup(Entity entity)
    {
        onPickupablePickedUpEvent.Raise(this, null);


        if (pickupDialogue)
        {
            dialogueManager.StartDialogue(onPickupDialogueData);
        }

        if (screenFlash)
        {
            screenFlashComponent.FlashColor(screenFlashColor, screenFlashDuration);
        }
    }
}
