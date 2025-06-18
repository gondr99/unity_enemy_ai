using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Action<Vector2> OnMouseMove;
    public Action OnReloadKeyPress;
    public Action<bool> OnFireKeyChange;
    public Vector2 Movement {get; private set;}
    private Controls _controls;

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
}
