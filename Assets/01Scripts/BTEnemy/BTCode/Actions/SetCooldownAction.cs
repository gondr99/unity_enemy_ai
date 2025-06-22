using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetCooldown", story: "Set [variable] to Time", category: "Certificate/Action", id: "eb653ebcffa2f427e4e05a331a01f20f")]
    public partial class SetCooldownAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> Variable;

        protected override Status OnStart()
        {
            Variable.Value = Time.time;
            return Status.Success;
        }
    }
}

