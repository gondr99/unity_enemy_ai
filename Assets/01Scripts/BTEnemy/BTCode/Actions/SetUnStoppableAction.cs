using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetUnStoppable", story: "[Self] to Unstoppable [IsActive]", category: "Action", id: "e1a09717d6cd316d5143c665e2ce6647")]
public partial class SetUnStoppableAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<bool> IsActive;

    protected override Status OnStart()
    {
        Self.Value.IsUnStoppable = IsActive.Value;
        return Status.Success;
    }
}

