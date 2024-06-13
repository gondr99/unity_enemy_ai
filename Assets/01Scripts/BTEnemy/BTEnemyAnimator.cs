
public class BTEnemyAnimator : AgentAnimator
{
    public EnemyAnimationTrigger TriggerCompo {get; private set;}

    private BTEnemy _enemy;
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as BTEnemy;

        TriggerCompo = owner.VisualTrm.GetComponent<EnemyAnimationTrigger>();
        TriggerCompo.AnimationEndEvent += HandleAnimationEndEvent;
    }

    private void HandleAnimationEndEvent()
    {
        _enemy.BlackBoard.animationEnd = true;
    }
}
