using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    private DialogueManager dialogueManager;

    [Header("Settings")]
    [SerializeField] bool interactDialogue;
    [SerializeField] DialogueData onInteractDialogueData;
    [SerializeField] bool isForDoor;
    [SerializeField] string doorUniqueId;

    [Header("Events")]
    [SerializeField] GameEvent onInteractedEvent;


    private void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                Interact(entity);
            }
        }
    }

    public virtual void Interact(Entity entity)
    {
        if ( isForDoor )
        {
            onInteractedEvent?.Raise(this, new GameEventData.OnButtonPressed { id = doorUniqueId });
        }

        if ( interactDialogue )
        {
            dialogueManager.StartDialogue(onInteractDialogueData);
        }
    }

}
