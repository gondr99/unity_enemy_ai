using _01Scripts.BTEnemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.Serialization;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wandering", story: "[Self] wandering [waypoints]", category: "Cetificate/Action", id: "ee2a0d727ae1a8b9f7ad4e28d09f277e")]
public partial class WanderingAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<WayPoints> WayPoints;
    [SerializeReference] public BlackboardVariable<AgentMovement> Movement;

    private Vector3 _nextPoint;
    private Vector3 _moveDirection;
    private Vector3 _prevPosition;
    protected override Status OnStart()
    {
        _nextPoint = WayPoints.Value.GetNextWayPoint();
        _prevPosition = Self.Value.transform.position;
        
        GoToDestination();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CheckArrived())
        {
            Movement.Value.StopImmediately();
            return Status.Success;
        }
        return Status.Running;
    }

    private bool CheckArrived()
    {
        Vector3 position = Self.Value.transform.position;
        Vector3 prevDirection = (_nextPoint - _prevPosition).normalized;
        Vector3 nowDirection = (_nextPoint - position).normalized;

        _prevPosition = position;
        return Vector2.Dot(prevDirection, nowDirection) < 0;
    }

    private void GoToDestination()
    {
        _moveDirection = (_nextPoint - Self.Value.transform.position).normalized;
        Movement.Value.SetMovement(_moveDirection);
    }
    

    protected override void OnEnd()
    {
    }
}

