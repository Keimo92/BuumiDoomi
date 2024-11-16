using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Button Door ID")]
    [SerializeField] private string buttonId;

    [Header("Events")]
    [SerializeField] private GameEvent onButtonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.entityType == Entity.EntityMask.Player )
            {
                onButtonPressed?.Raise(this, new GameEventData.OnButtonPressed { id = buttonId });
            }
        }
    }
}