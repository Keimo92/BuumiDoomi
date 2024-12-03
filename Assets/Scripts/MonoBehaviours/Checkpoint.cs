using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameEvent onCheckpointReachedEvent;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entity.entityType is Entity.EntityMask.Player)
            {
                //Player has reached this checkpoint. We broadcast the new spawn location to anybody who listens
                onCheckpointReachedEvent.Raise(this, new GameEventData.OnCheckpointReached { position = transform.position });
            }
        }
    }
}
