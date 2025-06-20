using System;
using GondrLib.EventSystem;
using GondrLib.SoundSystem;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private GameEventChannelSo createChannel;
    [SerializeField] private GameEventChannelSo soundChannel;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private int maxHitCount = 10;
    [SerializeField] private SoundSO explosionSound;
    
    
    private float _damage;
    private Vector2 _direction;
    private Collider2D[] _hitResult;

    private void Awake()
    {
        _hitResult = new Collider2D[maxHitCount];
    }

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
        CheckDamageTarget();
        DestroyRocket();
    }

    private void CheckDamageTarget()
    {
        int cnt =Physics2D.OverlapCircle(transform.position, explosionRadius, targetFilter, _hitResult);
        
        for (int i = 0; i < cnt; i++)
        {
            if (_hitResult[i].TryGetComponent(out IDamageable damageable))
            {
                Vector2 direction = _hitResult[i].transform.position - transform.position;
                damageable.ApplyDamage(_damage, direction.normalized);
            }
        }
    }

    private void DestroyRocket()
    {
        VfxPlay playEvt = CreateEvents.VfxPlay.Initializer(impactEffect, transform.position);
        createChannel.RaiseEvent(playEvt);

        PlaySFXEvent sfxEvt = SoundEvents.PlaySfxEvent.Initializer(explosionSound, transform.position);
        soundChannel.RaiseEvent(sfxEvt);
        
        _myPool.Push(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
