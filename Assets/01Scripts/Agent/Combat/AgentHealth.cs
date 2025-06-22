using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IAgentComponent, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private ActionData actionData;
    
    private Agent _agent;
    public UnityEvent<float, float> OnHealthChange;
    public void Initialize(Agent agent)
    {
        _agent = agent;
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float damage, Vector2 direction, float knockBackForce, Agent owner)
    {
        if (_agent.IsDead) return;
        
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (actionData != null)
        {
            actionData.targetTrm = owner.transform;
            actionData.LastHitDirection = direction;
            actionData.KnockBackForce = knockBackForce;
        }
        
        _agent.OnHit?.Invoke();

        OnHealthChange?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            _agent.OnDead?.Invoke();
        }
    }
}
