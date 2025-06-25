using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetComponent", story: "Get component from [self]", category: "Certificate/Action", id: "b84d621d84598546c1108b2ff47bc14b")]
public partial class GetComponentAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;

    protected override Status OnStart()
    {
        List<BlackboardVariable> variableList = Self.Value.BTAgent.BlackboardReference.Blackboard.Variables;

        foreach (BlackboardVariable variable in variableList)
        {
            if (typeof(IAgentComponent).IsAssignableFrom(variable.Type) ==false) continue;

            IAgentComponent targetComponent =Self.Value.GetCompo(variable.Type);
            Debug.Assert(targetComponent != null, $"{variable.Name} is not exist on {Self.Value.gameObject.name}");
            variable.ObjectValue = targetComponent;
        }
        return Status.Success;
    }

    
}

