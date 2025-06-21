using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Agent : MonoBehaviour, IDamageable
{
    public UnityEvent OnHit;
    public UnityEvent OnDead;
    public bool IsDead { get; protected set; }
    
    private Dictionary<Type, IAgentComponent> _componentDict = new Dictionary<Type, IAgentComponent>();

    public UnityEvent<float, Vector2, float, Agent> OnDamage;
    protected virtual void Awake()
    {
        _componentDict = GetComponentsInChildren<IAgentComponent>()
            .ToDictionary(compo => compo.GetType(), compo => compo);
        
        InitializeComponents();
    }

    protected virtual void InitializeComponents()
    {
        foreach(IAgentComponent compo in _componentDict.Values)
        {
            compo.Initialize(this);
        }
    }

    public T GetCompo<T>(bool isDerived = false) where T : IAgentComponent
    {
        Type type = typeof(T);
        if (_componentDict.TryGetValue(type, out IAgentComponent component))
        {
            return (T)component;
        }

        if (isDerived)
        {
            Type findType = _componentDict.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));
            if(findType != null)
                return (T)_componentDict[findType];
        }
        
        Debug.LogError($"Component of type {type} not found on agent {name}");
        return default;
    }

    public void ApplyDamage(float damage, Vector2 direction, float knockBackForce, Agent owner)
    {
        OnDamage?.Invoke(damage, direction, knockBackForce, owner);
    }
}
