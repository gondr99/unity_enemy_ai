using System;
using GondrLib.EventSystem;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [field: SerializeField] public GameEventChannelSo CreateChannel { get; private set; }
    [SerializeField] private PoolManagerMono poolManager;
    private void Awake()
    {
        CreateChannel.AddListener<ProjectileCreate>(HandleProjectileCreate);
    }

    private void HandleProjectileCreate(ProjectileCreate evt)
    {
        Projectile projectile = poolManager.Pop<Projectile>(evt.itemSo);
        projectile.InitAndFire(evt.fireTrm, evt.damage);
    }
}
