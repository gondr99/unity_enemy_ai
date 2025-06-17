using DG.Tweening;
using UnityEngine;

public class EnemyGolem : BTEnemy
{
    private readonly int _jumpAtkHash = Animator.StringToHash("JumpATK");
    private readonly int _dropAtkHash = Animator.StringToHash("DropATK");
    private readonly int _swingHash = Animator.StringToHash("Swing");

    protected AgentRenderer _rendererCompo;
    protected AgentMovement _moveCompo;
    protected AgentAnimator _agentAnimator;

    protected override void Awake()
    {
        base.Awake();
        _rendererCompo = GetCompo<AgentRenderer>(true);
        _moveCompo = GetCompo<AgentMovement>();
        _agentAnimator = GetCompo<AgentAnimator>(true);
    }

    public void JumpAttack(Vector3 targetPos)
    {
        _rendererCompo.SetSortingToTop(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(targetPos, 4f, 1, 1.2f).SetEase(Ease.Linear));
        seq.JoinCallback(() => _agentAnimator.SetTrigger(_jumpAtkHash));
        seq.InsertCallback(1f, () => _agentAnimator.SetTrigger(_dropAtkHash));
        seq.OnComplete(() =>
        {
            BlackBoard.isAttacking = false;
            _rendererCompo.SetSortingToTop(false);
        });
    }

    public void DashAttack(Vector3 targetPos)
    {
        _rendererCompo.SetSortingToTop(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOShakePosition(0.3f, 0.3f, 20));
        seq.Append(transform.DOMove(targetPos, 0.8f).SetEase(Ease.InCubic));
        seq.OnComplete(() =>
        {
            BlackBoard.isAttacking = false;
            _rendererCompo.SetSortingToTop(false);
        });
    }

    public void MeleeAttack()
    {
        _moveCompo.StopImmediately();
        _agentAnimator.SetTrigger(_swingHash);
    }
}
