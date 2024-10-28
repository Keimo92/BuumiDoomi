using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentController : MonoBehaviour
{
    [Header("NavAgent")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject EnemyObj;
    [SerializeField] float AgentSpeed = 10f;
    [SerializeField] ExampleEntity ExampleEntity;
    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private EntityFov Fov;

    public float AggroTime;

    //public bool HasTakenDamage = false;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        //HasTakenDamage = false;
    }
    
    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void MoveToRandomPositionInRadius(float radius)
    {
        Vector2 randomPosVec2 = Random.insideUnitCircle * radius;
        Vector3 randomPos = new Vector3(randomPosVec2.x, 0, randomPosVec2.y) + transform.position;
        agent.SetDestination(randomPos);
    }

    public void StopMovement()
    {
        agent.Stop();
    }
    public void MoveToPlayer(Transform playerPos)
    {
        float step = AgentSpeed * Time.deltaTime; 
        EnemyObj.transform.position = Vector3.MoveTowards(transform.position, playerPos.position, step);
    }

    private void Update()
    {
        if ( ExampleEntity.HasTakenDamage )
        {
           StartCoroutine(AggroCoroutine());
        }
    }

    private IEnumerator AggroCoroutine()
    {
        //TODO:  Fix Aggro time
        MoveToPlayer(PlayerPosition);
        Fov.ShootAtPlayer();
        yield return new WaitForSeconds(AggroTime);
        ExampleEntity.HasTakenDamage = false;
    }
}
