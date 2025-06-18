using System;
using UnityEngine;

[Serializable]
public class ActionData : MonoBehaviour, IAgentComponent
{
    public Transform targetTrm;

    private Agent _owner;
    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
