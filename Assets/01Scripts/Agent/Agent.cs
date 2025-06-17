using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    private Dictionary<Type, IAgentComponent> _componentDict = new Dictionary<Type, IAgentComponent>();

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
}
