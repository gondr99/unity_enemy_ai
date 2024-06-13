using System.Collections;
using System.Collections.Generic;
using BTVisual;
using UnityEngine;

public class WaitUntil : ActionNode
{
    public enum WaitType {
        AttackEnd,
        AnimationEnd,
        Cooldown,
    }

    public WaitType waitType;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        switch (waitType)
        {
            case WaitType.AttackEnd:
                return blackboard.isAttacking ? State.RUNNING : State.SUCCESS;
            case WaitType.AnimationEnd:
                return blackboard.animationEnd ? State.SUCCESS : State.RUNNING;
            case WaitType.Cooldown:
                //not yet
                break;
        }
        return State.FAILURE;
    }
}
