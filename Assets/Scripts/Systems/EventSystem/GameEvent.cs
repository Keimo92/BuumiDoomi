using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    //Raise Event
    public void Raise(Component sender, object data)
    {
        for(int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }

    //Manage Listeners
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}

//Here we define a struct of data for every type of event
//All event types can be found from the project folder
public class GameEventData
{
    public struct ExampleEvent
    {
        public string Data;
    }

    public struct OnButtonPressed
    {
        public string id;
    }

    public struct OnCheckpointReached
    {
        public Vector3 position;
    }
}
