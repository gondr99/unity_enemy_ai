using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Collider2D[] _colliders;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        _colliders = new Collider2D[1]; //Detect array
    }

    public override void Update()
    {
        int count = Physics2D.OverlapCircle(
            _enemy.transform.position, _enemy.detectRadius, 
            _enemy.enemyFilter, _colliders);
        
        if(count >= 1)
        {
            _actionData.targetTrm = _colliders[0].transform;
            _stateMachine.ChangeState((int)NinjaState.Move);
        }
    }
}
