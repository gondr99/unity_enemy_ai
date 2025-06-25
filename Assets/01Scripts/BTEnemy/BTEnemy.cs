using System;
using GondrLib.EventSystem;
using Unity.Behavior;
using UnityEngine;

public abstract class BTEnemy : Agent
{
    [field: SerializeField] public GameEventChannelSo CreateChannel;
    [field: SerializeField] public GameEventChannelSo SoundChannel;
    public float detectRadius;

    private BehaviorGraphAgent _btAgent;
    public BehaviorGraphAgent BTAgent => _btAgent;
    
    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        _btAgent = GetComponent<BehaviorGraphAgent>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}