using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
  
    public Entity.EntityMask entityMask;

    public bool DisableShake = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entityMask.HasFlag(entity.entityType))
            {
                entity.Damage(5);
                
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
