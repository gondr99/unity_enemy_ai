using GondrLib.ObjectPool.RunTime;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayEffect", story: "[Self] Play [Effect]", category: "Certificate/Action", id: "207ade9cddf24378213d00871f4a2be9")]
public partial class PlayEffectAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<PoolItemSO> Effect;
    [SerializeReference] public BlackboardVariable<Vector3> Offset;

    protected override Status OnStart()
    {
        var evt = CreateEvents.VfxPlay.Initializer(Effect.Value, Self.Value.transform.position + Offset.Value);
        Self.Value.CreateChannel.RaiseEvent(evt);
        return Status.Success;
    }
}

