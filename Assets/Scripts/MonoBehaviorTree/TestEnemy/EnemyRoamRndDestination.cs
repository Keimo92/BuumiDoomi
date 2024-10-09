using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

[AddComponentMenu("")]
// Register node in visual editor node finder
[MBTNode(name = "Tasks/EnemyRoamRandomPos")]

public class EnemyRoamRndDestination : Leaf
{
    [SerializeField] NavAgentController NavController;
    public float RoamRadius;

    public override NodeResult Execute()
    {
        if ( NavController != null )
        {
            NavController.MoveToRandomPositionInRadius(RoamRadius);
        }
        return NodeResult.success;
    }

    public override void OnExit()
    {
        
    }

}
