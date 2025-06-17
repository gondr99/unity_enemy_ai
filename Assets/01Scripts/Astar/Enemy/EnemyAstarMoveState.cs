using System.Collections;
using System.Collections.Generic;
using Gondr.Astar;
using UnityEngine;

public class EnemyAstarMoveState : EnemyState
{
    private readonly int _hashVelocity = Animator.StringToHash("Velocity");
    private AstarEnemy _agent;
    private List<Vector3> _path;
    private int _currentIndex;
    private Vector3 _nextPos;
    private Vector3 _beforeTargetPosition;
    private Vector3 _targetPosition;
    
    private AgentMovement _movementCompo;

    public EnemyAstarMoveState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _agent = enemy as AstarEnemy;
        _movementCompo = enemy.GetCompo<AgentMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        if(_enemy.ActionData.targetTrm == null) {
            _stateMachine.ChangeState(FSMState.Idle.ToString());
            return;            
        }

        SetTargetPosition();

        ChaseToTarget();
        

        _animatorCompo.SetFloat(_hashVelocity, _movementCompo.Velocity.magnitude);        
    }

    private void ChaseToTarget()
    {
        if(IsInPosition())
        {
            _movementCompo.StopImmediately();
            if(Time.time > _enemy.lastAttackTime + _enemy.attackCooldown) {
                _stateMachine.ChangeState(FSMState.Attack.ToString());
            }
            return;
        }

        float distance = Vector2.Distance(_nextPos, _enemy.transform.position);
        if(distance < _agent.stopDistance) //목표 도착 다음 목표 설정
        {
            _currentIndex++;
            if(_currentIndex < _path.Count)
                _nextPos = _path[_currentIndex];
        }

        Vector2 direction = _nextPos - _enemy.transform.position;
        _movementCompo.SetMovement(direction.normalized);
    }

    private bool IsInPosition()
    {
        float positionThreshold = 0.3f;
        Vector3 delta = _targetPosition - _enemy.transform.position;
        return Mathf.Abs(delta.x) < positionThreshold && Mathf.Abs(delta.y) < positionThreshold;
    }

    private void SetTargetPosition()
    {
        
        ActionData data = _enemy.ActionData;

        Vector3 targetPos = data.targetTrm.position;
        Vector3 myPos = _enemy.transform.position;

        float xDirection = Mathf.Sign(targetPos.x - myPos.x);
        targetPos.x -= xDirection; //Set destination to target's side

        _targetPosition = targetPos;

        if( Vector2.Distance( _targetPosition, _beforeTargetPosition) > _agent.stopDistance) 
        {
            _beforeTargetPosition = _targetPosition;
            ResetPath();
        }
    }

    private void ResetPath()
    {
        _path = _agent.GetPathToTarget(_agent.ActionData.targetTrm.position);
        _currentIndex = 0;
        if(_path.Count > 0)
            _nextPos = _path[_currentIndex];
        else
            _nextPos = _targetPosition;
    }
}
