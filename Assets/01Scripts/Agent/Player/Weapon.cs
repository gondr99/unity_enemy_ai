using System;
using GondrLib.EventSystem;
using UnityEngine;
using UnityEngine.Events;


public class Weapon : MonoBehaviour
{
    [field:SerializeField] public WeaponDataSO WeaponData { get; private set; }
    [SerializeField] private GameEventChannelSo createChannel;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private int headUpSortingOrder = -1;
    [field: SerializeField] public Transform FireTrm { get; private set; }

    public UnityEvent OnFireBullet;

    public int Ammo => _currentAmmo;
    private int _defaultSoringOrder;
    private float _lastFireTime;
    private int _currentAmmo;

    public void InitializeWeapon()
    {
        _defaultSoringOrder = weaponSprite.sortingOrder;
        _currentAmmo = WeaponData.maxAmmo;
    }

    public void FlipYWeapon(bool isFlip)
    {
        weaponSprite.flipY = isFlip;
    }

    public void SetHeadUp(float rotateAngle)
    {
        weaponSprite.sortingOrder = rotateAngle is > 45 and < 135 ? headUpSortingOrder : _defaultSoringOrder;
    }

    public bool TryToShooting()
    {
        if (_lastFireTime + WeaponData.cooldown > Time.time) { return false; }
        if (_currentAmmo <= 0) { return false; }

        FireSingleShot();
        return true;
    }

    private void FireSingleShot()
    {
        var createEvt = CreateEvents.ProjectileCreate.Initializer(FireTrm, WeaponData.damage,WeaponData.bulletSo);
        createChannel.RaiseEvent(createEvt);
        _lastFireTime = Time.time;
        _currentAmmo--;
        OnFireBullet?.Invoke();
    }
}
