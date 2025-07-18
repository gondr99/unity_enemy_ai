using System;
using UnityEngine;

[Serializable]
public class ActionData : MonoBehaviour, IAgentComponent
{
    public Transform targetTrm;
    public Vector2 LastHitDirection { get; set; }
    public float KnockBackForce { get; set; }

    private Agent _owner;
    public void Initialize(Agent agent)
    {
        _owner = agent;
    }
}
