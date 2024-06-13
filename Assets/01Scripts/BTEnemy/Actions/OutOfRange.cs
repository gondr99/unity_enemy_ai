using BTVisual;
using UnityEngine;

public class OutOfRange : ActionNode
{
    public float checkDistance = 5;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(blackboard.targetTrm == null) return State.FAILURE;
        
        Vector3 targetPos = blackboard.targetTrm.position;
        float distance = Vector2.Distance(targetPos, enemy.transform.position);

        if (distance > checkDistance) return State.SUCCESS;
        
        return State.FAILURE;
    }
}
