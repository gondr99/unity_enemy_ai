using System;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private int headUpSortingOrder = -1;
    [field: SerializeField] public Transform FireTrm { get; private set; }
    
    private int _defaultSoringOrder;

    public void InitializeWeapon()
    {
        _defaultSoringOrder = weaponSprite.sortingOrder;
    }

    public void FlipYWeapon(bool isFlip)
    {
        weaponSprite.flipY = isFlip;
    }

    public void SetHeadUp(float rotateAngle)
    {
        weaponSprite.sortingOrder = rotateAngle is > 45 and < 135 ? headUpSortingOrder : _defaultSoringOrder;
    }
}
