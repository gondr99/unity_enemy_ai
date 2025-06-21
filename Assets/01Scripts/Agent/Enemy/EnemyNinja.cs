public enum NinjaState : int
{
    Idle,
    Move,
    Attack,
    Hit,
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
        stateMachine.AddState((int)NinjaState.Hit, new EnemyHitState(this, stateMachine, "Hit"));
        stateMachine.AddState((int)NinjaState.Dead, new EnemyDeadState(this, stateMachine, "Dead"));
    }

    private void Start()
    {
        stateMachine.ChangeState((int)NinjaState.Idle);
    }

    public void SetHitState()
    {
        if (IsDead) return;
        
        stateMachine.ChangeState((int)NinjaState.Hit);
    }

    public void SetDeadState()
    {
        if (IsDead) return;

        IsDead = true;
        stateMachine.ChangeState((int)NinjaState.Dead);
    }
}
