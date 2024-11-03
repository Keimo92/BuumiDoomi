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
    [SerializeField] string animatorOnShootTriggerName;

    [Header("Animator")]
    [SerializeField] Animator animator;
    [SerializeField] string onShootTrigger;

    [Header("State")]
    [SerializeField] EnemyState enemyState;
    bool isShooting;

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
            case EnemyState.Aggressive:
                if(Time.time > lastShootTime + shootDelay && !isShooting)
                {
                    StartShootAnimation();
                }
                else if(!isShooting)
                {
                    navController.MoveToPosition(playerEntity.transform.position);
                }
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
        //We don't do anything when idle
        if (enemyState == EnemyState.Idle) return;

        //For optimization purposes we do raycasts only when the player is close enough
        if (Vector3.Distance(playerEntity.transform.position, transform.position) < detectionRadius) 
        {
            Vector3 dirToPlayer = (playerEntity.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hitInfo, detectionRadius))
            {
                //If we see the player we get angery >:( 
                if (hitInfo.transform == playerEntity.transform) 
                {
                    enemyState = EnemyState.Aggressive;
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

    //This is called when we want to start the shooting
    public void StartShootAnimation()
    {
        //Stop movement and set isShooting to true
        isShooting = true;
        navController.StopMovement();

        //Rotate towards player
        transform.LookAt(playerEntity.transform.position);
        Vector3 eulerAnglers = transform.eulerAngles;
        eulerAnglers.z = 0;
        eulerAnglers.x = 0;
        transform.rotation = Quaternion.Euler(eulerAnglers);

        //Start animation in the animator
        animator.SetTrigger(animatorOnShootTriggerName);
    }

    //This is called by an animation event.
    public void AnimEventShootProjectile()
    {
        //
        // This is called on a specific frame on the shoot animation so we can time the projectile with the animation
        //
        
        //Shoot logic
        GameObject go = Instantiate(bulletPf, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = (playerEntity.transform.position - bulletSpawn.position).normalized * bulletSpeed;
        lastShootTime = Time.time;
    }

    //This is called by an animation event.
    public void AnimEventShootEnd()
    {
        //
        // We want to continue movement only when the shooting is done. This is called at the end of the animation by an animation event.
        //

        isShooting = false; //Set is shooting to false when the shoot animation has finished
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
        if(enemyState != EnemyState.Aggressive) //If the player shoots at us when we are not aggressive.
        {
            playerLastSeenTime = Time.time; //We mark up the last time we have seen the player here. Means that we will de aggro after the deAggro time has passed.
            StartShootAnimation(); //We want to shoot back instantly to communicate that we are angery >:(
            enemyState = EnemyState.Aggressive; //We start to be aggressive
        }
        
        base.Damage(damage);
    }
}
