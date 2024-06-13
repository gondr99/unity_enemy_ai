using UnityEngine;

public class BTEnemyRenderer : AgentRenderer
{
    private BTEnemy _enemy;
    
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as BTEnemy;
    }

    protected override void UpdateVisual()
    {
        Transform target = _enemy.BlackBoard.targetTrm;
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
