using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckInPlayer", story: "[Self] Check [Target] in Detect range", category: "Conditions", id: "97ef13ecfc5d2f92393ff488ab8a1186")]
public partial class CheckInPlayerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    public override bool IsTrue()
    {
        return Vector2.Distance(Self.Value.transform.position, Target.Value.position) < Self.Value.detectRadius;
    }
}
