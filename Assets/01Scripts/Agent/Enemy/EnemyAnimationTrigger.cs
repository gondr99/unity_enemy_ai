using System;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour, IAgentComponent
{
    public event Action AnimationEndEvent;
    public event Action AttackCasteEvent;

    private Agent _owner;

    private void OnAnimationEndTrigger()
    {
        AnimationEndEvent?.Invoke();
    }

    private void OnAttackCast()
    {
        AttackCasteEvent?.Invoke();
    }

    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
