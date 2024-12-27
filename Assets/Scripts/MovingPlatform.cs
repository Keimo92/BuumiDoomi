using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovingPlatform : MonoBehaviour
{    
    [SerializeField] BoxCollider trigger;

    [SerializeField] Transform lerpPointA, lerpPointB;
    [SerializeField] float lerpTime;
    Vector3 velocity;
    PlayerMovement playerMovement;
    private void Start()
    {
        //Freeze all rb movement as we only want collision detection
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //Set the lerp poins Y axis to transform pos. Currently the vertical movement of moving platforms is very bad. 
        lerpPointA.position = new Vector3(lerpPointA.position.x, transform.position.y, lerpPointA.position.z);
        lerpPointB.position = new Vector3(lerpPointB.position.x, transform.position.y, lerpPointB.position.z);
    }

    float t;
    Vector3 oldPosition;
    private void Update()
    {
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(lerpPointA.position, lerpPointB.position, Mathf.PingPong(t/lerpTime, 1)); //Lerp between the two positions
        velocity = transform.position - oldPosition; //Velocity to move the player onboard later
        oldPosition = transform.position;
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (playerMovement != null)
        {
            playerMovement.SetExternalMovement(velocity);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.SetExternalMovement(velocity);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMovement.SetExternalMovement(Vector3.zero); //when player leaves the platform. Leave.
            playerMovement = null;
        }
    }
}
