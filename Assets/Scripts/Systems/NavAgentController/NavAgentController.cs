using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentController : MonoBehaviour
{
    [Header("NavAgent")]
    [SerializeField] NavMeshAgent agent;

    [SerializeField] float AgentSpeed = 10f;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
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
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, step);
    }
}
