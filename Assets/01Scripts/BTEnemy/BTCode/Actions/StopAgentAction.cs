using Gondr.Astar;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopAgent", story: "Set [Agent] Stop to [IsStop]", category: "Certificate/Action", id: "d8c371d339bce2dfddaf08b27e87290c")]
public partial class StopAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<AstarAgent> Agent;
    [SerializeReference] public BlackboardVariable<bool> IsStop;

    protected override Status OnStart()
    {
        Agent.Value.IsStop = IsStop.Value;
        return Status.Success;
    }
}

