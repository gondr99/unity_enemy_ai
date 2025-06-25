using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "InAttackDistance", story: "can attack between [Self] and [Target] in [range]", category: "Conditions", id: "87278bcb413ab4461089fc3601d68e34")]
public partial class InAttackDistanceCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Range;

    public override bool IsTrue()
    {
        Vector2 delta = Target.Value.position - Self.Value.transform.position;
        return Mathf.Abs(delta.x) < Range.Value && Mathf.Abs(delta.y) < 0.5f;
    }

}
