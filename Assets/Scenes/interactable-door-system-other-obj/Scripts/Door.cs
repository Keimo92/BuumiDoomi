using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door ID")]
    public string doorUniqueId;

    [Header("Settings for opening speed and position")] //This is just testing only that they open. We can use better mechanics here, but did not wanna use time for it that much. <3
    public float openingSpeed; 
    public Vector3 offset;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private static Dictionary<string, List<Door>> doorId = new Dictionary<string, List<Door>>();
    private bool isOpening = false;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + offset;

        RegisterDoor(doorUniqueId, this);
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

    public void OpenDoor(Component sender, object data)
    {
        if ( data is GameEventData.OnDoorButtonPressed eventData && eventData.id == doorUniqueId )
        {
            OpenAllDoorsWithId(eventData.id);
            Debug.Log("These doors opened" + doorUniqueId); 
        }
    }

    //Register doors with id
    public void RegisterDoor(string id, Door door)
    {
        if ( !doorId.ContainsKey(id) )
        {
            doorId[id] = new List<Door>();
        }
        doorId[id].Add(door);
    }

    //If door has same id as the button it will open all, otherwise only that what is assigned.
    public void OpenAllDoorsWithId(string id)
    {
        if ( doorId.TryGetValue(id, out List<Door> doors) )
        {
            foreach ( Door door in doors )
            {
                door.isOpening = true;
            }
        }
    }
}
