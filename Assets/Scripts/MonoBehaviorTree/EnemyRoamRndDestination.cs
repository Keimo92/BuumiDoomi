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
    public override NodeResult Execute()
    {
        if ( NavController != null )
        {
            NavController.MoveToRandomPositionInRadius(20f);
        }
        return NodeResult.success;
    }

}
