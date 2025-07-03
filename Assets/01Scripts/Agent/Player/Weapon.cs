using System;
using System.Collections;
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
    public Action<float> OnReloadValueChange;
    public Action<bool> OnReloadStatusChange;

    public int Ammo => _currentAmmo;
    public bool IsReloading { get; private set; } = false;
    
    private int _defaultSoringOrder;
    private float _lastFireTime;
    private int _currentAmmo;

    private Agent _gunOwner;
    public void InitializeWeapon(Agent owner)
    {
        _gunOwner = owner;
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
        var createEvt = CreateEvents.ProjectileCreate.Initializer(FireTrm, WeaponData.damage,WeaponData.bulletSo, _gunOwner);
        createChannel.RaiseEvent(createEvt);
        _lastFireTime = Time.time;
        _currentAmmo--;
        OnFireBullet?.Invoke();
    }

    public void TryReload()
    {
        if(Ammo >= WeaponData.maxAmmo || IsReloading) return;
        ReloadRoutine();
        
    }

    private async void ReloadRoutine()
    {
        IsReloading = true;
        OnReloadStatusChange?.Invoke(IsReloading);
        float currentTime = 0f;
        float percent = 0;
        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / WeaponData.reloadTime;
            OnReloadValueChange?.Invoke(percent);
            await Awaitable.NextFrameAsync();
        }
        _currentAmmo = WeaponData.maxAmmo;
        IsReloading = false;
        OnReloadStatusChange?.Invoke(IsReloading);
    }
}
