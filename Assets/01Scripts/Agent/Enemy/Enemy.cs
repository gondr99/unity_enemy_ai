using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Detect Setting Values")]
    public ContactFilter2D enemyFilter;
    public float detectRadius;

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    [HideInInspector] public float lastAttackTime;


    public EnemyStateMachine stateMachine;
    [field:SerializeField] public ActionData ActionData {get; private set;}

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
        ActionData = new ActionData();
    }

    protected virtual void Update()
    {
        stateMachine.UpdateMachine();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.white;
    }
}
