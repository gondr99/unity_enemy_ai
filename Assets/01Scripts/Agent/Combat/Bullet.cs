using System;
using UnityEngine;


public class Bullet : Projectile 
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float lifeTime = 2f;

    private float _damage;
    private Vector2 _direction;
    
    public override void InitAndFire(Transform firePos, float damage)
    {
        _damage = damage;
        transform.SetPositionAndRotation(firePos.position, firePos.rotation);
        _direction = firePos.right;
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = _direction * moveSpeed;
        _lifeTimer += Time.fixedDeltaTime;

        if (_lifeTimer >= lifeTime)
        {
            _isDead = true;
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
        _isDead = true;
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        _myPool.Push(this);
    }
}
