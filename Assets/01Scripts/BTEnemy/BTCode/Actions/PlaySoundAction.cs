using System;
using GondrLib.SoundSystem;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlaySound", story: "[Self] Play [sfx]", category: "Action", id: "55d2e1a5d4cff0df2e13b6c1ad63ff06")]
public partial class PlaySoundAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<SoundSO> Sfx;

    protected override Status OnStart()
    {
        var evt = SoundEvents.PlaySfxEvent.Initializer(Sfx.Value, Self.Value.transform.position);
        Self.Value.SoundChannel.RaiseEvent(evt);
        return Status.Success;
    }
}

