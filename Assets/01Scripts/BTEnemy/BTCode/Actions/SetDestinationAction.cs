using Gondr.Astar;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDestination", story: "[Self] Navigate to [Destination] with [AstarAgent]",
    category: "Certificate/Action", id: "c28b7c7fd425996a91784a9ba20d1f45")]
public partial class SetDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<AstarAgent> AstarAgent;
    [SerializeReference] public BlackboardVariable<Vector3> Destination;

    protected override Status OnStart()
    {
        AstarAgent.Value.SetDestination(Destination.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AstarAgent.Value.IsArrived)
            return Status.Success;
        return Status.Running;
    }

}

