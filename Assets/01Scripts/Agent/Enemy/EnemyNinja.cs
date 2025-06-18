using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NinjaState : int
{
    Idle,
    Move,
    Attack,
    Dead
}

public class EnemyNinja : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.AddState((int)NinjaState.Idle , new EnemyIdleState(this, stateMachine, "Idle"));
        stateMachine.AddState((int)NinjaState.Move, new EnemyMoveState(this, stateMachine, "Move"));
        stateMachine.AddState((int)NinjaState.Attack, new EnemyAttackState(this, stateMachine, "Attack"));
    }

    private void Start()
    {
        stateMachine.ChangeState((int)NinjaState.Idle);
    }
}
