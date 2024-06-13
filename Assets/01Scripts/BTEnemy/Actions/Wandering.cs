using BTVisual;
using UnityEngine;

public class Wandering : ActionNode
{
    protected override void OnStart()
    {
        GetNextPoint();
    }

    protected override void OnStop()
    {
        
    }

    private bool GetNextPoint()
    {
        return MapManager.Instance.GetRandomFromPosition(enemy.transform.position, out blackboard.moveToPosition);
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetTrm != null) 
            return State.FAILURE;

        Vector3 direction = blackboard.moveToPosition - enemy.transform.position;
        float distance = direction.magnitude;
        float threshold = 0.2f;

        if(distance < threshold) {
            return State.SUCCESS;
        }

        enemy.MoveCompo.SetMovement(direction.normalized);
        return State.RUNNING;
    }
}
