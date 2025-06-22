using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CooldownCheck", story: "[sec] have passed since the [lastTime]", category: "Conditions", id: "652603b85494ecef7769f43a2ee37944")]
public partial class CooldownCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> Sec;
    [SerializeReference] public BlackboardVariable<float> LastTime;

    public override bool IsTrue()
    {
        return LastTime.Value + Sec.Value < Time.time;
    }

}
