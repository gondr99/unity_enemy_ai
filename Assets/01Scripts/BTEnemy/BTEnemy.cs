using BTVisual;
using UnityEngine;

public abstract class BTEnemy : Agent
{
    [Header("Detect Setting Values")]
    public ContactFilter2D enemyFilter;
    public float detectRadius;

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    [HideInInspector] public float lastAttackTime;

    public EnemyAnimationTrigger AnimTriggerCompo {get; private set;}
    public BehaviourTreeRunner TreeRunner {get; private set;}

    public BlackBoard BlackBoard => TreeRunner.tree.blackboard;

    protected override void Awake()
    {
        base.Awake();

        TreeRunner = GetComponent<BehaviourTreeRunner>();
        
    }

}