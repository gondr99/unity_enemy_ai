using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private AgentMovement _movement;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _movement = enemy.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        _movement.StopImmediately();

    }

    public override void Update()
    {
        base.Update();
        if(_animationEndTrigger) {
            _stateMachine.ChangeState((int)NinjaState.Move);
        }
    }

    public override void Exit()
    {
        _enemy.lastAttackTime = Time.time;
        base.Exit();
    }
}
