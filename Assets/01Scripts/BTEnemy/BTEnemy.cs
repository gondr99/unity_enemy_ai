using System;
using GondrLib.EventSystem;
using Unity.Behavior;
using UnityEngine;

public abstract class BTEnemy : Agent
{
    [field: SerializeField] public GameEventChannelSo CreateChannel;
    [field: SerializeField] public GameEventChannelSo SoundChannel;
    public float detectRadius;

    [SerializeField] private NinjaStateChannel stateChannel;
    private BehaviorGraphAgent _btAgent;
    public BehaviorGraphAgent BTAgent => _btAgent;

    public bool IsUnStoppable { get; set; }
    
    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        _btAgent = GetComponent<BehaviorGraphAgent>();
    }

    protected virtual void Start()
    {
        if(_btAgent.GetVariable("StateChannel", out BlackboardVariable<NinjaStateChannel> stateChannelVariable))
        {
            stateChannel = stateChannelVariable.Value;
        }
        
        OnHit.AddListener(HandleHitEvent);
        OnDead.AddListener(HandleDeadEvent);
    }

    protected virtual void OnDestroy()
    {
        OnHit.RemoveListener(HandleHitEvent);
        OnDead.RemoveListener(HandleDeadEvent);
    }

    private void HandleDeadEvent()
    {
        if (IsDead) return;
        IsDead = true;
        stateChannel.SendEventMessage(NinjaBTState.DEAD);
    }

    private void HandleHitEvent()
    {
        if (IsUnStoppable || IsDead) return;
        stateChannel.SendEventMessage(NinjaBTState.HIT);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}