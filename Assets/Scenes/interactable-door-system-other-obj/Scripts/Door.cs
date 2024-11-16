using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door ID")]
    [SerializeField] string doorId;

    [Header("Settings for opening speed and position")] //This is just testing only that they open. We can use better mechanics here, but did not wanna use time for it that much. <3
    [SerializeField] float openingSpeed;
    [SerializeField] Vector3 openedOffset; //Offset after the door is opened. 

    //Private vars
    private Vector3 targetPosition;
    private bool isOpening = false;

    private void Start()
    {
        targetPosition = transform.position + openedOffset;
    }

    private void Update()
    {
        if ( isOpening )
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openingSpeed * Time.deltaTime);

            if ( transform.position == targetPosition )
            {
                isOpening = false;
            }
        }
    }

    //This is called by the OnButtonPressed event (SO Event System)
    public void OnButtonPressed(Component sender, object data)
    {
        if ( data is GameEventData.OnButtonPressed eventData )
        {
            if(eventData.id == doorId) //If the id matches this doors id -> Open
            {
                isOpening = true; // -> Update loop
                Debug.Log("Door opened: " + transform); 
            }
            
        }
    }
}
