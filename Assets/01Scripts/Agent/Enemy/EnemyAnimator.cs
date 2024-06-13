
public class EnemyAnimator : AgentAnimator
{
    public EnemyAnimationTrigger TriggerCompo {get; private set;}

    private Enemy _enemy;
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as Enemy;

        TriggerCompo = owner.VisualTrm.GetComponent<EnemyAnimationTrigger>();
        TriggerCompo.AnimationEndEvent += HandleAnimationEndEvent;
    }

    private void HandleAnimationEndEvent()
    {
        _enemy.stateMachine.currentState.AnimationEndTrigger();
    }
}
