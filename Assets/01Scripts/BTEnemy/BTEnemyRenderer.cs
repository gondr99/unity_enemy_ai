using UnityEngine;

public class BTEnemyRenderer : AgentRenderer
{
    private BTEnemy _enemy;
    
    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _enemy = owner as BTEnemy;
    }
    
}
