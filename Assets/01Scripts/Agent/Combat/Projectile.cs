using System;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;


public abstract class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] protected ContactFilter2D targetFilter;
    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected PoolItemSO impactEffect;
    
    protected bool _isDead;
    protected float _lifeTimer;
    protected Pool _myPool;

    [field:SerializeField] public PoolItemSO PoolItem { get; private set; }
    public GameObject GameObject => gameObject;
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public virtual void ResetItem()
    {
        _isDead = false;
        _lifeTimer = 0;
    }

    public abstract void InitAndFire(Transform firePos, float damage);
    
}
