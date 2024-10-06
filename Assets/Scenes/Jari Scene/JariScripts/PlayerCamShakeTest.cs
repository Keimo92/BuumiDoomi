using UnityEngine;


public class PlayerCamShakeTest : MonoBehaviour
{
    public ICameraShaker CameraShake;
    public int health = 100;

    private void Start()
    {
        CameraShake = FindObjectOfType<CameraController>();
    }

    public void TakeDamage(int damage)
    {

        if ( health > 0 )
        {
            if ( CameraShake != null )
            {
                CameraShake.ShakeCamera(2f, 0.5f);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("EnemyBullet") )
        {
            Debug.Log("Collided with player");
            TakeDamage(10);
        }
    }
}

