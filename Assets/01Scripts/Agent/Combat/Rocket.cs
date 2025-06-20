using GondrLib.EventSystem;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private GameEventChannelSo createChannel;
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
            DestroyRocket();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
        _isDead = true;
        DestroyRocket();
    }

    private void DestroyRocket()
    {
        VfxPlay playEvt = CreateEvents.VfxPlay.Initializer(impactEffect, transform.position);
        createChannel.RaiseEvent(playEvt);
        _myPool.Push(this);
    }
}
