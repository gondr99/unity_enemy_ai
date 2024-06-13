using BTVisual;
using UnityEngine;

public class CheckObstacle : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 targetPos = blackboard.targetTrm.position;
        Vector3 start = enemy.transform.position;
        Vector3 direction = targetPos - start;

        RaycastHit2D hit = Physics2D.Raycast(start, direction.normalized, direction.magnitude, blackboard.whatIsObstacle);
        Debug.DrawRay(start, direction);
        Debug.Log(hit.collider);
        if(hit.collider == null) return State.SUCCESS;

        return State.FAILURE;
    }
}
