using UnityEngine;
using MBT;
using System.Collections;
using UnityEngine.AI;

// Empty Menu attribute prevents Node to show up in "Add Component" menu.
[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "Tasks/EnemyShootPlayer")]
public class CustomTask : Leaf
{

    public Transform Player;
    public Transform BulletSpawn;

    [SerializeField] NavMeshAgent Agent;
    public Transform EnemyObj;
    public GameObject Bullet;
    public float shootingRange = 15f;
    public float shootingInterval = 1f;
    public float DistanceToPlayer;
    public float bulletSpeed = 10f;
    public float nextShotTime;
    public override NodeResult Execute()
    {
        if ( Player != null )
        {
            DistanceToPlayer = Vector3.Distance(EnemyObj.transform.position, Player.position);
            

            // Check if the player is within shooting range
            if ( DistanceToPlayer <= shootingRange )
            {
                ShootAtPlayer();
            }
     
        }

        return NodeResult.success;
    }
    private void ShootAtPlayer()
    {
        EnemyObj.transform.LookAt(Player.position);
        GameObject bulletInstance = Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity);

        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        bulletRb.velocity = directionToPlayer * bulletSpeed;
    }
}
