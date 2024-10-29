using UnityEngine;



public class EnemyBullet : MonoBehaviour
{

 
    ScreenFlash ScreenFlash;
    Entity Player;
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
                Destroy(gameObject,0.4f);
                ScreenFlash.FlashColor(Color.red, 0.2f);
            }


        }
        if ( other.gameObject.CompareTag("Wall") )
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,0.4f);
        }
    }
}
