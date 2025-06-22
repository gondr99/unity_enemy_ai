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
        _rendererCompo.SetSortingToTop(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOShakePosition(0.3f, 0.3f, 20));
        seq.Append(transform.DOMove(targetPos, 0.8f).SetEase(Ease.InCubic));
        seq.OnComplete(() =>
        {
            
            _rendererCompo.SetSortingToTop(false);
        });
    }

    public void MeleeAttack()
    {
        _moveCompo.StopImmediately();
        //_agentAnimator.SetTrigger(_swingHash);
    }
}
