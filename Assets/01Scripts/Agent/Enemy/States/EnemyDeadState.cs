using Object = UnityEngine.Object;

public class EnemyDeadState : EnemyState
{
    private AgentMovement _movement;
    private EnemyAnimationTrigger _trigger;
    private bool _isAnimationEnd;
    
    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _movement = enemy.GetCompo<AgentMovement>();
        _trigger = enemy.GetCompo<EnemyAnimationTrigger>();
    }

    public override void Enter()
    {
        base.Enter();
        _isAnimationEnd = false;
        _movement.StopImmediately();
        _movement.CanManualMove = false;
        _trigger.AnimationEndEvent += HandleAnimationEnd;
    }

    public override void Exit()
    {
        _trigger.AnimationEndEvent += HandleAnimationEnd;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (_isAnimationEnd)
            Object.Destroy(_enemy.gameObject);
    }

    private void HandleAnimationEnd() => _isAnimationEnd = true;
}
