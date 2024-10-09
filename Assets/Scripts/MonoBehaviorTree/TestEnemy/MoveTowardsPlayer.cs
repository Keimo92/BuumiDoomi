using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using System.Linq;

[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "Tasks/MoveTowardsPlayer")]
public class MoveTowardsPlayer : Leaf
{
    [SerializeField] NavAgentController NavController;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Transform EnemyObj;
    public float shootingRange = 15f;
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override NodeResult Execute()
    {
        if ( NavController != null )
        {
            float DistanceToPlayer = Vector3.Distance(EnemyObj.transform.position, PlayerPos.transform.position);
            if ( DistanceToPlayer > shootingRange )
            {
                NavController.MoveToPlayer(PlayerPos);
            }
        }
        return NodeResult.success;
    }
}
