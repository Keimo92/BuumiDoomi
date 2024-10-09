using UnityEngine;


public class PlayerCamShakeTest : MonoBehaviour
{
    public ICameraShaker CameraShake;

    private void Start()
    {
        CameraShake = FindObjectOfType<CameraController>();
    }

    public void TakeDamage(int damage)
    {
        if ( CameraShake != null )
        {
            CameraShake.ShakeCamera(2f, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "EnemyBullet" )
        {
            Debug.Log("Collided with player");
            TakeDamage(10);
        }
    }
}

