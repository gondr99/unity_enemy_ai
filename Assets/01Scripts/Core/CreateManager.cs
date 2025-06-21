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
        CreateChannel.AddListener<VfxPlay>(HandleVfxPlay);
    }

    private void OnDestroy()
    {
        CreateChannel.RemoveListener<ProjectileCreate>(HandleProjectileCreate);
        CreateChannel.RemoveListener<VfxPlay>(HandleVfxPlay);
    }

    private void HandleVfxPlay(VfxPlay evt)
    {
        VfxPlayer player = poolManager.Pop<VfxPlayer>(evt.effectSo);
        player.PlayVfx(evt.position);
    }

    private void HandleProjectileCreate(ProjectileCreate evt)
    {
        Projectile projectile = poolManager.Pop<Projectile>(evt.itemSo);
        projectile.InitAndFire(evt.fireTrm, evt.damage, evt.owner);
    }
}
