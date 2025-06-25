using UnityEngine;

public class EnemyHitState: EnemyState
{
    private AgentMovement _movement;

    private const float MAX_KNOCKBACK_FORCE = 7f;
    private float _knockBackEndTime;
    public EnemyHitState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _movement = enemy.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        _knockBackEndTime = Time.time + Mathf.Lerp(0.1f, 0.5f, _actionData.KnockBackForce / MAX_KNOCKBACK_FORCE); 
        _movement.CanManualMove = false;
        _movement.StopImmediately();
        _movement.AddForceToAgent(_actionData.LastHitDirection * _actionData.KnockBackForce);
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= _knockBackEndTime)
        {
            _stateMachine.ChangeState((int)NinjaState.Move);
        }
    }

    public override void Exit()
    {
        _movement.CanManualMove = true;
        base.Exit();
    }
}
