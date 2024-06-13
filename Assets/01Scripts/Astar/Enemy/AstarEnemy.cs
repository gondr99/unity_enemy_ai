using System.Collections;
using System.Collections.Generic;
using Gondr.Astar;
using UnityEngine;

public class AstarEnemy : Enemy
{
    public AstarAgent AStarCompo {get; protected set;}
    public float stopDistance = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        AStarCompo = GetComponent<AstarAgent>();
    }

    public List<Vector3> GetPathToTarget(Vector3 target)
    {
        AStarCompo.SetDestination(target);
        return AStarCompo.GetPath();
    }
}
