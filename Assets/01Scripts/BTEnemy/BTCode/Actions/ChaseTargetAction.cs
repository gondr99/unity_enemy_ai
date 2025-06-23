using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "ChaseTarget", story: "[Self] Chase to [target] in [Sec]", category: "Certificate/Action", id: "acacbc9f1c973d49f42502dfc3df2a02")]
    public partial class ChaseTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyGolem> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<float> Sec;

        private AgentMovement _movement;
        private float _startTime;
        private Vector3 _targetPosition;
        
        protected override Status OnStart()
        {
            Initialize();
            _startTime = Time.time;
            return Status.Running;
        }

        private void Initialize()
        {
            if (_movement == null)
                _movement = Self.Value.GetCompo<AgentMovement>();
            
        }

        protected override Status OnUpdate()
        {
            SetTargetPosition();
            Vector2 direction = (_targetPosition - Self.Value.transform.position).normalized;
            _movement.SetMovement(direction);

            if (_startTime + Sec.Value < Time.time)
            {
                return Status.Failure;
            }
            
            return Status.Running;
        }
        
        private void SetTargetPosition()
        {
            Vector3 targetPos = Target.Value.position;
            Vector3 myPos = Self.Value.transform.position;

            float xDirection = Mathf.Sign(targetPos.x - myPos.x);
            targetPos.x -= xDirection; //Set destination to target's side

            _targetPosition = targetPos;

        }
    }
}

