using UnityEngine;

public class EnemyState
{
    protected int _animatorHash;
    protected bool _animationEndTrigger;

    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string stateName)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _animatorHash = Animator.StringToHash(stateName);
    }

    public virtual void Enter()
    {
        _enemy.AnimatorCompo.SetBool(_animatorHash, true);
        _animationEndTrigger = false;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        _enemy.AnimatorCompo.SetBool(_animatorHash, false);
    }

    public void AnimationEndTrigger()
    {
        _animationEndTrigger = true;
    }
}
