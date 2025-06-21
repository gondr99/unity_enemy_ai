using GondrLib.EventSystem;
using GondrLib.ObjectPool.RunTime;
using UnityEngine;

public static class CreateEvents
{
    public static ProjectileCreate ProjectileCreate = new ProjectileCreate();
    public static VfxPlay VfxPlay = new VfxPlay();
}

public class ProjectileCreate : GameEvent
{
    public Transform fireTrm;
    public float damage;
    public PoolItemSO itemSo;
    public Agent owner;

    public ProjectileCreate Initializer(Transform trm, float damage, PoolItemSO item, Agent owner)
    {
        fireTrm = trm;
        this.damage = damage;
        itemSo = item;
        this.owner = owner;
        return this;
    }
}

public class VfxPlay : GameEvent
{
    public PoolItemSO effectSo;
    public Vector3 position;

    public VfxPlay Initializer(PoolItemSO item, Vector3 position)
    {
        this.effectSo = item;
        this.position = position;
        return this;
    }
}
