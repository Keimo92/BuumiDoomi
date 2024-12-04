using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent OnLevelFinished;

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
                OnLevelFinished.Raise(this, null);
            }
        }
    }
}
