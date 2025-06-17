using UnityEngine;

public class EnemyState
{
    protected int _animatorHash;
    protected bool _animationEndTrigger;

    protected Enemy _enemy;
    protected EnemyStateMachine _stateMachine;
    protected AgentAnimator _animatorCompo;
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string stateName)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _animatorCompo = enemy.GetCompo<AgentAnimator>(true);
        _animatorHash = Animator.StringToHash(stateName);
    }

    public virtual void Enter()
    {
        _animatorCompo.SetBool(_animatorHash, true);
        _animationEndTrigger = false;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        _animatorCompo.SetBool(_animatorHash, false);
    }

    public void AnimationEndTrigger()
    {
        _animationEndTrigger = true;
    }
}
