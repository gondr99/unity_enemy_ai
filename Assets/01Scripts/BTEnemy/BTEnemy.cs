using UnityEngine;

public abstract class BTEnemy : Agent
{
    [Header("Detect Setting Values")]
    public ContactFilter2D enemyFilter;
    public float detectRadius;

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    [HideInInspector] public float lastAttackTime;
    
    
    protected override void Awake()
    {
        base.Awake();

    
        
    }

}