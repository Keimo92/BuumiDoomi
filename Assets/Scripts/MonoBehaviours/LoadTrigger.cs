using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class LoadTrigger : MonoBehaviour
{
    [SerializeField] List<Transform> loadablesList = new List<Transform>();
    [SerializeField] List<Transform> unLoadablesList = new List<Transform>();

    private void Start()
    {
        //Setup collision
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entity.entityType is Entity.EntityMask.Player)
            {
                foreach(Transform t in loadablesList)
                {
                    if(t != null) t.gameObject.SetActive(true);
                    
                }
                foreach (Transform t in unLoadablesList)
                {
                    if (t != null) t.gameObject.SetActive(false);
                }
            }
        }
    }
}
