using System;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour, IAgentComponent
{
    private Player _player;
    [field: SerializeField] public Weapon CurrentWeapon { get; private set; }
    public void Initialize(Agent agent)
    {
        _player = agent as Player;
    }

    private void Update()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        
    }
}
