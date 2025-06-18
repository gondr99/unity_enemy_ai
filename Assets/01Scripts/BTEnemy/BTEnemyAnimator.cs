
public class BTEnemyAnimator : AgentAnimator
{
    public EnemyAnimationTrigger TriggerCompo {get; private set;}

    private BTEnemy _enemy;
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as BTEnemy;

        TriggerCompo = GetComponent<EnemyAnimationTrigger>();
        TriggerCompo.AnimationEndEvent += HandleAnimationEndEvent;
    }

    private void HandleAnimationEndEvent()
    {
        
    }
}
