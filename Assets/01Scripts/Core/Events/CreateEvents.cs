using GondrLib.EventSystem;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

public static class CreateEvents
{
    public static ProjectileCreate ProjectileCreate = new ProjectileCreate();
}

public class ProjectileCreate : GameEvent
{
    public Transform fireTrm;
    public float damage;
    public PoolItemSO itemSo;

    public ProjectileCreate Initializer(Transform trm, float damage, PoolItemSO item)
    {
        fireTrm = trm;
        this.damage = damage;
        itemSo = item;
        return this;
    }
}
