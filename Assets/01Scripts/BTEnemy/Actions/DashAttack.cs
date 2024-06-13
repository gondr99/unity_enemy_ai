using System.Collections;
using System.Collections.Generic;
using BTVisual;
using UnityEngine;

public class DashAttack : ActionNode
{
    private EnemyGolem golem;

    protected override void OnStart()
    {
        golem = enemy as EnemyGolem;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        blackboard.isAttacking = true;
        golem.DashAttack(blackboard.targetTrm.position);
        return State.SUCCESS;
    }
}
