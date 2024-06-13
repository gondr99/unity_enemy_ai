using BTVisual;

public class JumpAttack : ActionNode
{
    private EnemyGolem golem;

    protected override void OnStart()
    {
        golem = enemy as EnemyGolem;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        blackboard.isAttacking = true;
        golem.JumpAttack(blackboard.targetTrm.position);
        return State.SUCCESS;
    }

  
}
