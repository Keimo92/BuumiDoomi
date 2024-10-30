using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavAgentController))]
public class ProjectileEnemyEntity : Entity
{
    //Derives basic behaviour from the Entity class.
    public enum EnemyState
    {
        Aggressive,
        Roaming,
        Idle
    }

    [Header("Basic Setup")]
    [SerializeField] PlayerEntity playerEntity;

    [Header("Settings")]
    [SerializeField] float detectionRadius;
    [SerializeField] GameObject bulletPf;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float shootDelay;
    [SerializeField] float bulletSpeed;
    [SerializeField] float deAggroTime;

    [Header("State")]
    [SerializeField] EnemyState enemyState;

    //UnSerialized variables
    NavAgentController navController; //Lets keep this since we can be sure that the entity has a navController because of RequireComponent
    float lastShootTime;
    float playerLastSeenTime;

    private void Start()
    {
        navController = GetComponent<NavAgentController>();
        InvokeRepeating("CheckVisionToPlayer", 0f, 0.1f); //Check vision to player 10 times a sec
    }

    private void Update()
    {
        //We change behaviour based on the state of the enemy
        switch (enemyState)
        {
            case EnemyState.Idle: //We don't do anything when idle
                break;
            case EnemyState.Aggressive: //When aggressive we shoot when shootdelay has passed and move towards the player
                if(Time.time > lastShootTime + shootDelay)
                {
                    Shoot();
                }
                navController.MoveToPosition(playerEntity.transform.position);
                break;
            case EnemyState.Roaming: //When roaming we just move to random position in radius
                if (!navController.IsMoving())
                {
                    navController.MoveToRandomPositionInRadius(10f);
                }
                break;
        }
    }

    private void CheckVisionToPlayer()
    {
        if (enemyState == EnemyState.Idle) return; //We don't do anything when idle
        if (Vector3.Distance(playerEntity.transform.position, transform.position) < detectionRadius) //For optimization purposes we do raycasts only when the player is close enough
        {
            Vector3 dirToPlayer = (playerEntity.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hitInfo, detectionRadius))
            {
                if (hitInfo.transform == playerEntity.transform) //If we see the player -> Aggro
                {
                    enemyState = EnemyState.Aggressive; 
                    playerLastSeenTime = Time.time;
                }
                else
                {
                    if (Time.time > playerLastSeenTime + deAggroTime) //If the time we have last seen the player exceeds deAggroTime we go back to roaming
                    {
                        enemyState = EnemyState.Roaming;
                    }
                }
            }
        }
    }

    public void Shoot()
    {
        lastShootTime = Time.time;

        //Shoot logic
        GameObject go = Instantiate(bulletPf, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = (playerEntity.transform.position - bulletSpawn.position).normalized * bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        if(enemyState == EnemyState.Aggressive)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    //When we want we can override the parent class behaviour by overriding the functions
    public override void Damage(float damage)
    {
        //We need a way to filter on what is the damage source somehow but this will deal for now
        enemyState = EnemyState.Aggressive;
        base.Damage(damage);
    }
}
