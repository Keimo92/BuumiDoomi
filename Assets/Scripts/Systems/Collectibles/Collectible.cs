using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private DialogueData pickUpData;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private BoxCollider boxCol;
    public event Action<Collectible> OnCollectiblePickedUp;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        manager = FindFirstObjectByType<DialogueManager>();
        boxCol.isTrigger = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                OnCollectiblePickedUp.Invoke(this);
                manager.StartDialogue(pickUpData);
                Destroy(gameObject);
            }
        }
    }

}