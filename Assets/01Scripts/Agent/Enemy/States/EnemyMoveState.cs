using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Vector3 _targetPosition;
    private readonly int _hashVelocity = Animator.StringToHash("Velocity");

    private AgentMovement _movementCompo;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _movementCompo = enemy.GetCompo<AgentMovement>();
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
        Vector2 direction = _targetPosition - _enemy.transform.position;
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

    }
}
