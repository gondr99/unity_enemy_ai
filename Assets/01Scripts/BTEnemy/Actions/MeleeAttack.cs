using System.Collections;
using System.Collections.Generic;
using BTVisual;
using UnityEngine;

public class MeleeAttack : ActionNode
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
        blackboard.animationEnd = false;
        golem.MeleeAttack();
        return State.SUCCESS;
    }
}
