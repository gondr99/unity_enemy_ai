using System;
using Unity.Behavior;
using UnityEngine;

public abstract class BTEnemy : Agent
{
    [Header("Detect Setting Values")]
    public ContactFilter2D enemyFilter;
    public float detectRadius;

    private BehaviorGraphAgent _btAgent;
    
    protected override void Awake()
    {
        base.Awake();
        _btAgent = GetComponent<BehaviorGraphAgent>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}