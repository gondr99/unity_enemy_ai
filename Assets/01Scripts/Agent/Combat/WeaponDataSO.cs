using GondrLib.ObjectPool.RunTime;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon/Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    public Sprite gunSprite;
    public string gunName;
    public Vector3 gunSpritePosition;
    public Weapon prefab;
    public PoolItemSO bulletSo;
    
    public float damage;
    public int maxAmmo;
    public float reloadTime;
    public float cooldown;
}
