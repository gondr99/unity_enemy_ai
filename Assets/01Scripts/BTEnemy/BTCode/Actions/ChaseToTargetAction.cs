using Gondr.Astar;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTarget", story: "[Self] Chase to [Target] with [Agent]", category: "Action", id: "4ee63f363658df2e5489df32388362a8")]
public partial class ChaseToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<AstarAgent> Agent;

    protected override Status OnStart()
    {
        //공격지점을 찾아야 해.(타겟의 좌우 지점으로)
        SetTargetPosition();
        return Status.Success;
    }
    
    private void SetTargetPosition()
    {
        Vector3 targetPos = Target.Value.position;
        Vector3 myPos = Self.Value.transform.position;

        float xDirection = Mathf.Sign(targetPos.x - myPos.x);
        targetPos.x -= xDirection; //Set destination to target's side

        //여기가 이동가능위치인지 판단해야해.
        if (AstarMapManager.Instance.CheckValidPosition(targetPos) == false)
        {
            targetPos.x += xDirection * 2; //반대쪽으로 설정.     
        }
        
        Agent.Value.SetDestination(targetPos); //1만큼 빠진위치로
    }
}

