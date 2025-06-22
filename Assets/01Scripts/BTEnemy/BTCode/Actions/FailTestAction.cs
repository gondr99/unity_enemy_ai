using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "FailTest", story: "Fail test", category: "Certificate/Action", id: "cd49a3dd22be6dfeb036be7b2b14cba8")]
    public partial class FailTestAction : Action
    {

        protected override Status OnStart()
        {
            Debug.Log("고의 실패 테스트");
            return Status.Failure;
        }
    }
}

