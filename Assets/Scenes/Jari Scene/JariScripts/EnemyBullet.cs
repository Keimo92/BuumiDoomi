using UnityEngine;



public class EnemyBullet : MonoBehaviour
{

    ICameraShaker Shaker;

    private void Start()
    {
        Shaker = FindObjectOfType<CameraController>();
    }

    

    void Damage(int damage)
    {
        if (Shaker != null)
        {
            Shaker.ShakeCamera(3f, 0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            Damage(1);
        }
    }
}
