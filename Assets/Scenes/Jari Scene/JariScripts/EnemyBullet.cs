using UnityEngine;



public class EnemyBullet : MonoBehaviour
{

    ICameraShaker Shaker;
    ScreenFlash ScreenFlash;
    private void Start()
    {
        Shaker = FindObjectOfType<CameraController>();
        ScreenFlash = FindAnyObjectByType<ScreenFlash>();
    }



    void Damage(int damage)
    {
        if ( Shaker != null )
        {
            Shaker.ShakeCamera(3f, 0.3f);
            StartCoroutine(ScreenFlash.SetColorToRed());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damage(1);
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
