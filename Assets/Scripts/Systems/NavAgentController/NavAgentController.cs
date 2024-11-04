using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentController : MonoBehaviour
{
    [Header("NavAgent")]
    [SerializeField] NavMeshAgent agent;


    private void Start()
    {
        if ( agent == null ) agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 position)
    {
        if (agent.isStopped) agent.isStopped = false;
        agent.SetDestination(position);
    }

    public void MoveToRandomPositionInRadius(float radius)
    {
        if (agent.isStopped) agent.isStopped = false;
        Vector2 randomPosVec2 = Random.insideUnitCircle * radius;
        Vector3 randomPos = new Vector3(randomPosVec2.x, 0, randomPosVec2.y) + transform.position;
        agent.SetDestination(randomPos);
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public bool IsMoving()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if(agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return false;
            }
        }
        return true;
    }
}
