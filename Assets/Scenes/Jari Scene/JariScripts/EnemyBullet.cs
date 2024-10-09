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
        if ( Shaker != null )
        {
            Shaker.ShakeCamera(3f, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            Damage(1);
            Destroy(this.gameObject);

        }
        if ( other.gameObject.CompareTag("Wall") )
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject,0.4f);
        }
    }
}
