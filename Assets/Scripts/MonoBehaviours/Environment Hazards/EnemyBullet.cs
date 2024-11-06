using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
  
    public Entity.EntityMask entityMask;

    public bool DisableShake = false;

    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entityMask.HasFlag(entity.entityType))
            {
                entity.Damage(damage);
                
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
