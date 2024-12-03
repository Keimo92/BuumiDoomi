using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Ravines : MonoBehaviour
{
    private BoxCollider boxcollider;
    private Rigidbody rb;

    private void Start()
    {
        // set these values to prevent ravines from dropping through the map.
        boxcollider = GetComponent<BoxCollider>();
        boxcollider.isTrigger = true;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            entity.Kill();
        }
    }
}
