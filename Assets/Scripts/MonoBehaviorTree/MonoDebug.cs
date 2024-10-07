using UnityEngine;
using MBT;
using System.Collections;

// Empty Menu attribute prevents Node to show up in "Add Component" menu.
[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "Tasks/EnemyShootPlayer")]
public class CustomTask : Leaf
{
    public Transform Player;
    public Transform BulletSpawn;
    private Animator animator;
    public GameObject Bullet;
    public float shootingRange = 15f;
    public float shootingInterval = 2f;

    public float bulletSpeed = 10f;


    private float nextShotTime;

    public override void OnEnter()
    {
        animator = GetComponentInParent<Animator>();
        nextShotTime = Time.time + shootingInterval;
    }

    public override NodeResult Execute()
    {
        if ( Player != null )
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            // Check if the player is within shooting range
            if ( distanceToPlayer <= shootingRange )
            {
                ShootAtPlayer();
                StartCoroutine(ResetAfterShoot());
            }
            else if ( distanceToPlayer > shootingRange )
            {
                animator.Play("Enemy_Idle");
            }
        }
        return NodeResult.success;
    }
    private void ShootAtPlayer()
    {
        animator.Play("Enemy_Shoot");
        GameObject bulletInstance = Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity);

        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        bulletRb.velocity = directionToPlayer * bulletSpeed;
    }
    IEnumerator ResetAfterShoot()
    {
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(nextShotTime);

    }
}
