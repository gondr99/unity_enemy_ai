using UnityEngine;

public class PlayerAnimator : AgentAnimator
{
    private readonly int _hashIsMove = Animator.StringToHash("IsMove");

    private AgentMovement _movementCompo;

    public override void Initialize(Agent owner)
    {
        base.Initialize(owner);
        _movementCompo = _owner.GetCompo<AgentMovement>();
    }

    private void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        bool isMove = _movementCompo.Velocity.sqrMagnitude > 0;
        _animator.SetBool(_hashIsMove, isMove);
    }

}
