using System.Collections;
using System.Collections.Generic;
using Gondr.Astar;
using UnityEngine;

public class NinjaAstar : AstarEnemy
{
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine.AddState((int)NinjaState.Idle, new EnemyIdleState(this, stateMachine, "Idle"));
        //stateMachine.AddState("Move", new EnemyMoveState(this, stateMachine, "Move"));
        stateMachine.AddState((int)NinjaState.Move, new EnemyAstarMoveState(this, stateMachine, "Move"));
        stateMachine.AddState((int)NinjaState.Attack, new EnemyAttackState(this, stateMachine, "Attack"));
    }

    private void Start()
    {
        stateMachine.ChangeState((int)NinjaState.Idle);
    }
}
