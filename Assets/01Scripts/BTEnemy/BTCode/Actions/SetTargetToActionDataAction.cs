using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTargetToActionData", story: "Set [Target] to [ActionData]", category: "Action", id: "582dd8c337d215c4343d560a8006421e")]
public partial class SetTargetToActionDataAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<ActionData> ActionData;

    protected override Status OnStart()
    {
        ActionData.Value.targetTrm = Target.Value;
        return Status.Success;
    }
}

