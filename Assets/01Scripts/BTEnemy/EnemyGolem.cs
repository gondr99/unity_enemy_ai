using DG.Tweening;
using UnityEngine;

public class EnemyGolem : BTEnemy
{
    private readonly int _jumpAtkHash = Animator.StringToHash("JumpATK");
    private readonly int _dropAtkHash = Animator.StringToHash("DropATK");
    private readonly int _swingHash = Animator.StringToHash("Swing");

    public void JumpAttack(Vector3 targetPos)
    {
        RenderCompo.SetSortingToTop(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(targetPos, 4f, 1, 1.2f).SetEase(Ease.Linear));
        seq.JoinCallback(() => AnimatorCompo.SetTrigger(_jumpAtkHash));
        seq.InsertCallback(1f, () => AnimatorCompo.SetTrigger(_dropAtkHash));
        seq.OnComplete(() =>
        {
            BlackBoard.isAttacking = false;
            RenderCompo.SetSortingToTop(false);
        });
    }

    public void DashAttack(Vector3 targetPos)
    {
        RenderCompo.SetSortingToTop(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOShakePosition(0.3f, 0.3f, 20));
        seq.Append(transform.DOMove(targetPos, 0.8f).SetEase(Ease.InCubic));
        seq.OnComplete(() =>
        {
            BlackBoard.isAttacking = false;
            RenderCompo.SetSortingToTop(false);
        });
    }

    public void MeleeAttack()
    {
        MoveCompo.StopImmediately();
        AnimatorCompo.SetTrigger(_swingHash);
    }
}
