using UnityEngine;

public class EnemyRenderer : AgentRenderer
{
    private Enemy _enemy;
    private ActionData _actionData;
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as Enemy;
        _actionData = owner.GetCompo<ActionData>();
    }

    protected override void UpdateVisual()
    {
        Transform target = _actionData.targetTrm;
        if (target == null)
        {
            base.UpdateVisual();
        }
        else
        {
            bool isTurnLeft = isFacingRight && target.position.x < transform.position.x;
            bool isTurnRight = !isFacingRight && target.position.x > transform.position.x;
            if (isTurnLeft || isTurnRight)
                Flip();
        }
    }
}
