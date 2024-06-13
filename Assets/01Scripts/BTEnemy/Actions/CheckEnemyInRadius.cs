using System.Collections;
using System.Collections.Generic;
using BTVisual;
using UnityEngine;

public class CheckEnemyInRadius : ActionNode
{
    private Collider2D[] _colliders = new Collider2D[1];

    protected override void OnStart()
    {
        Debug.Log("Check start");
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        int count = Physics2D.OverlapCircle(enemy.transform.position, enemy.detectRadius, enemy.enemyFilter, _colliders);

        if(count > 0)
        {
            blackboard.targetTrm = _colliders[0].transform;
            blackboard.spotTime = Time.time; //발견한 시간 기록
        }

        return State.SUCCESS;
    }
}
