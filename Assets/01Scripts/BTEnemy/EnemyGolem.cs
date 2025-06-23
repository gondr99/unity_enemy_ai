using DG.Tweening;
using UnityEngine;

public class EnemyGolem : BTEnemy
{
    
    
    private readonly int _swingHash = Animator.StringToHash("Swing");

    protected AgentRenderer _rendererCompo;
    protected AgentMovement _moveCompo;
    

    protected override void Awake()
    {
        base.Awake();
        _rendererCompo = GetCompo<AgentRenderer>(true);
        _moveCompo = GetCompo<AgentMovement>();
        
    }

    public void JumpAttack(Vector3 targetPos)
    {
        
    }

    public void DashAttack(Vector3 targetPos)
    {
        
    }

    public void MeleeAttack()
    {
        _moveCompo.StopImmediately();
        //_agentAnimator.SetTrigger(_swingHash);
    }
}
