using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "WaitForAnimation", story: "Wait for [Trigger]", category: "Certificate/Action", id: "b5ac1f19b01800db83198ac5187952a2")]
    public partial class WaitForAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyAnimationTrigger> Trigger;

        private bool _isEnd;
        protected override Status OnStart()
        {
            _isEnd = false;
            Trigger.Value.AnimationEndEvent += HandleAnimationEnd;
            return Status.Running;
        }

        private void HandleAnimationEnd()
        {
            _isEnd = true;
        }

        protected override Status OnUpdate()
        {
            if(_isEnd)
                return Status.Success;
            
            return Status.Running;
        }

        protected override void OnEnd()
        {
            Trigger.Value.AnimationEndEvent -= HandleAnimationEnd;
        }
    }
}

