using _01Scripts.BTEnemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetNextWayPoint", story: "Set [NextPoint] from [Waypoints]", category: "Certificate/Action", id: "8a7383b4ae2627a76744eb1cab0eb27b")]
public partial class SetNextWayPointAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> NextPoint;
    [SerializeReference] public BlackboardVariable<WayPoints> Waypoints;

    protected override Status OnStart()
    {
        NextPoint.Value = Waypoints.Value.GetNextWayPoint();
        return Status.Success;
    }
}

