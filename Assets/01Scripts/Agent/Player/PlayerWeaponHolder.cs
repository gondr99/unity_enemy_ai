using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponHolder : MonoBehaviour, IAgentComponent
{
    private Player _player;
    [field: SerializeField] public Weapon CurrentWeapon { get; private set; }
    [field: SerializeField] public InputReader PlayerInput { get; private set; }

    [SerializeField] private WeaponDataSO[] weaponList;
    public UnityEvent<Weapon, Weapon> WeaponChange;

    private Dictionary<int, Weapon> _weaponDict;
    private bool _isShooting = false;
    public void Initialize(Agent agent)
    {
        _player = agent as Player;
        _weaponDict = new Dictionary<int, Weapon>();
        MakeWeapons();
        ChangeGun(0);

        PlayerInput.OnChangeWeapon += ChangeGun;
        PlayerInput.OnFireKeyChange += HandleFireState;
    }

    private void OnDestroy()
    {
        PlayerInput.OnChangeWeapon -= ChangeGun;
        PlayerInput.OnFireKeyChange -= HandleFireState;
    }

    private void HandleFireState(bool isShooting) => _isShooting = isShooting;

    public void ChangeGun(int idx)
    {
        if (_weaponDict.ContainsKey(idx) == false) return;
        
        //여기서 기존 구독 처리 다 해줘야 해.
        Weapon prevWeapon = CurrentWeapon;
        prevWeapon?.gameObject.SetActive(false);
        CurrentWeapon = _weaponDict[idx];
        CurrentWeapon.gameObject.SetActive(true);
        WeaponChange?.Invoke(prevWeapon, CurrentWeapon);
    }

    private void MakeWeapons()
    {
        for (int idx = 0; idx < weaponList.Length; idx++)
        {
            Weapon newWeapon = Instantiate(weaponList[idx].prefab, transform);
            newWeapon.InitializeWeapon(_player);
            newWeapon.transform.localPosition = weaponList[idx].gunSpritePosition;
            newWeapon.gameObject.SetActive(false);
            _weaponDict.Add(idx, newWeapon);
        }
    }

    private void Update()
    {
        RotateWeapon(PlayerInput.MousePosition);
        CheckFire();
    }

    private void CheckFire()
    {
        if (_isShooting && CurrentWeapon != null)
        {
            CurrentWeapon.TryToShooting();
        }
    }

    private void RotateWeapon(Vector2 mousePos)
    {
        Vector2 direction = _player.transform.InverseTransformPoint(mousePos);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        CurrentWeapon.FlipYWeapon(angle is >= 90f or < -90f);
        CurrentWeapon.SetHeadUp(angle);
    }
}
