using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Action OnReloadKeyPress;
    public Action<bool> OnFireKeyChange;
    public Action<int> OnChangeWeapon;
    public Vector2 Movement {get; private set;}
    public Vector2 MousePosition { get; private set; }
    
    private Controls _controls;
    private Camera _mainCam;
    public Camera MainCam
    {
        get
        {
            if (_mainCam == null)
                _mainCam = Camera.main;
            return _mainCam;
        }
    }
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        MousePosition = MainCam.ScreenToWorldPoint(mousePosition);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnFireKeyChange?.Invoke(true);
        else if(context.canceled)
            OnFireKeyChange?.Invoke(false);
            
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnReloadKeyPress?.Invoke();
    }

    public void OnWeaponOne(InputAction.CallbackContext context)
    {
        OnChangeWeapon?.Invoke(0);
    }

    public void OnWeaponTwo(InputAction.CallbackContext context)
    {
        OnChangeWeapon?.Invoke(1);
    }
}
