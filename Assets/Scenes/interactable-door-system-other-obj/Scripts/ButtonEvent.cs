using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    [Header("Button Door ID")]
    [SerializeField] private string doorToOpenId; 

    [SerializeField] private GameEvent onDoorButtonPressed;
    [SerializeField] private GameEvent doorOpen;

    public void OnDoorButtonPressed(Component sender, object data)
    {
        if ( data is GameEventData.OnDoorButtonPressed eventData )
        {
            doorOpen.Raise(this,null);
            Debug.Log("Event raised for door open");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                onDoorButtonPressed?.Raise(this, new GameEventData.OnDoorButtonPressed { id = doorToOpenId });
               

            }
        }
    }
}