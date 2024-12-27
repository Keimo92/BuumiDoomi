using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerExample : MonoBehaviour
{
    public void PrintHello(Component sender, object data)
    {
        if(data is GameEventData.ExampleEvent eventData)
        {
            Debug.Log(eventData.Data);
        }
    }
}
