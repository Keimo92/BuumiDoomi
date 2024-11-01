using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    ScreenFlash ScreenFlash;
    public Entity.EntityMask entityMask;

    public bool DisableShake = false;
    private void Start()
    {
     
        ScreenFlash = FindAnyObjectByType<ScreenFlash>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entityMask.HasFlag(entity.entityType))
            {
                entity.Damage(5);
                
                ScreenFlash.FlashColor(Color.red, 0.2f);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
