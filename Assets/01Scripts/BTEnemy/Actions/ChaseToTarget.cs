using BTVisual;
using UnityEngine;

public class ChaseToTarget : ActionNode
{
    public float chaseTime = 3f;
    public float attackRange = 1f;

    protected override void OnStart()
    {
        blackboard.startChaseTime = Time.time;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        //일정시간 이상 쫒았다면 더이상 쫒지 말고 공격
        if(Time.time > blackboard.startChaseTime + chaseTime ) {
            return State.SUCCESS;
        }

        Vector3 targetPos = blackboard.targetTrm.position;
        Vector3 myPos = enemy.transform.position;

        Vector3 direction = targetPos - myPos;
        float distance = direction.magnitude;
        if(distance < attackRange)
            return State.SUCCESS;
        
        enemy.MoveCompo.SetMovement(direction.normalized);

        return State.RUNNING;
    }
}
