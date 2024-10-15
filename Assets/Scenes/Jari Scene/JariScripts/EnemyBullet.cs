using UnityEngine;



public class EnemyBullet : MonoBehaviour
{

    ICameraShaker Shaker;
    ScreenFlash ScreenFlash;
    Entity Player;
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
            StartCoroutine(ScreenFlash.SetColorToRed());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            entity.Damage(5);
            ShakeCameraOnHit();
            Destroy(gameObject,0.4f);

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
