using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRaiserExample : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;

    private void Start()
    {
        gameEvent.Raise(this, new GameEventData.ExampleEvent { Data = "Hello" });
    }
}
