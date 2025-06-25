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

    public override void UpdateVisual(float xValue)
    {
        Transform target = _actionData.targetTrm;
        if (target == null)
        {
            base.UpdateVisual(xValue);
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
