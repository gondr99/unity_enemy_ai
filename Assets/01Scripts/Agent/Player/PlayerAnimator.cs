using UnityEngine;

public class PlayerAnimator : AgentAnimator
{
    private readonly int _hashIsMove = Animator.StringToHash("IsMove");

    private void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        bool isMove = _owner.MoveCompo.RigidCompo.linearVelocity.sqrMagnitude > 0;
        _animator.SetBool(_hashIsMove, isMove);
    }

}
