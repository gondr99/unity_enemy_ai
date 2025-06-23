using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StopMove", story: "Stop [movement]", category: "Certificate/Action", id: "8040151d87abfc42a1a26dbcd40ba7f2")]
    public partial class StopMoveAction : Action
    {
        [SerializeReference] public BlackboardVariable<AgentMovement> Movement;

        protected override Status OnStart()
        {
            Movement.Value.StopImmediately();
            return Status.Success;
        }
    }
}

