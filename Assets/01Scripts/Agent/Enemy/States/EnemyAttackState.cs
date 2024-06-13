using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MoveCompo.StopImmediately();

    }

    public override void Update()
    {
        base.Update();
        if(_animationEndTrigger) {
            _stateMachine.ChangeState(FSMState.Move.ToString());
        }
    }

    public override void Exit()
    {
        _enemy.lastAttackTime = Time.time;
        base.Exit();
    }
}
