using UnityEngine;



public class EnemyBullet : MonoBehaviour
{

    ICameraShaker Shaker;
    ScreenFlash ScreenFlash;
    Entity Player;
    public Entity.EntityMask entityMask;
    private void Start()
    {
        Shaker = FindObjectOfType<CameraController>();
        ScreenFlash = FindAnyObjectByType<ScreenFlash>();
    }



    void ShakeCameraOnHit()
    {
        if ( Shaker != null )
        {
            Shaker.ShakeCamera(3f, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            if(entityMask.HasFlag(entity.entityType))
            {
                entity.Damage(5);
                ShakeCameraOnHit();
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
