using _01Scripts.BTEnemy.NinjaBT;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeClip", story: "Change to [clip] in [renderer]", category: "Action", id: "703ee9062494fd412b29c1ea2abadbc0")]
public partial class ChangeClipAction : Action
{
    [SerializeReference] public BlackboardVariable<AnimParamSo> Clip;
    [SerializeReference] public BlackboardVariable<AgentAnimator> Renderer;

    protected override Status OnStart()
    {
        Renderer.Value.ChangeClip(Clip.Value.hashValue);
        return Status.Success;
    }
}

