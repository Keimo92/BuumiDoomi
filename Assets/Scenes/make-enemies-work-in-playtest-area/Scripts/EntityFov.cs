using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityFov : MonoBehaviour
{
    [SerializeField] NavAgentController NavController;
    public NavMeshAgent agent;
    public Transform Player;
    public Transform EnemyObj;
    public GameObject Bullet;
    public Transform BulletSpawn;
    // Vision
    public float detectionRange = 10f;
    public float fieldOfView = 120f;
    public float shootCooldown = 1f;  
    private float lastShootTime = 0f; 

    public float bulletSpeed;



    public LayerMask obstacleMask;
    public LayerMask playerMask;

    public bool playerInSight;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckPlayerInSight();

        if ( playerInSight )
        {
            ShootAtPlayer();
        }

    }

    void CheckPlayerInSight()
    {
        playerInSight = false;


        Vector3 directionToPlayer = (Player.position - transform.position).normalized;


        float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);


        if ( angleBetweenEnemyAndPlayer < fieldOfView / 2f )
        {

            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            if ( distanceToPlayer <= detectionRange )
            {

                if ( !Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask) )
                {

                    playerInSight = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfView / 2, transform.up) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfView / 2, transform.up) * transform.forward * detectionRange;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
    public void ShootAtPlayer()
    {
        
        if ( Time.time >= lastShootTime + shootCooldown )
        {
            
            lastShootTime = Time.time;

            //TODO: This needs to have smoother transform! Slerp?
            EnemyObj.transform.LookAt(Player.position);

           
            GameObject bulletInstance = Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity);
            Vector3 directionToPlayer = (Player.position - transform.position).normalized;

            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            bulletRb.velocity = directionToPlayer * bulletSpeed;
        }
    }
}
