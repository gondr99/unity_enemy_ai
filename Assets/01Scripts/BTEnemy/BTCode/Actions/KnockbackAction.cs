using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Knockback", story: "[Self] Knockback by [Actiondata]", category: "Action", id: "d16f0430a94cf161029a3c791434be78")]
public partial class KnockbackAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<ActionData> Actiondata;

    private const float MAX_KNOCKBACK_FORCE = 7f;
    private float _knockBackEndTime;
    private AgentMovement _movement;
    
    protected override Status OnStart()
    {
        InitComponents();
        var actionData = Actiondata.Value;
        _knockBackEndTime = Time.time + Mathf.Lerp(0.1f, 0.5f, actionData.KnockBackForce / MAX_KNOCKBACK_FORCE);
        _movement.StopImmediately();
        _movement.CanManualMove = false;
        _movement.AddForceToAgent(actionData.LastHitDirection * actionData.KnockBackForce);
        return Status.Running;
    }

    private void InitComponents()
    {
        if (_movement == null)
        {
            _movement = Self.Value.GetCompo<AgentMovement>();
        }
    }

    protected override Status OnUpdate()
    {
        if (Time.time >= _knockBackEndTime)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        _movement.CanManualMove = true;
        _movement.StopImmediately();
    }
}

