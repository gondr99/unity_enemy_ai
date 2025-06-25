
public class EnemyAnimator : AgentAnimator
{
    public EnemyAnimationTrigger TriggerCompo {get; private set;}

    private Enemy _enemy;
    private EnemyAnimationTrigger _trigger;
    
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as Enemy;
        TriggerCompo = owner.GetCompo<EnemyAnimationTrigger>();
        TriggerCompo.AnimationEndEvent += HandleAnimationEndEvent;
    }

    private void HandleAnimationEndEvent()
    {
        _enemy.stateMachine.CurrentState.AnimationEndTrigger();
    }
    
    
}
