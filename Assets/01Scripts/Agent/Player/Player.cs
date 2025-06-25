using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    [field:SerializeField] public InputReader PlayerInput {get; private set;}

    public UnityEvent<Vector2> OnMoveKeyPressed;
    public UnityEvent<float> OnAimDirection;
    private void Update()
    {
        UpdateMoveInput();
        UpdateAimDirection();
    }

    private void UpdateAimDirection()
    {
        Vector2 direction = PlayerInput.MousePosition - (Vector2)transform.position;
        OnAimDirection?.Invoke(direction.x);
    }

    private void UpdateMoveInput()
    {
        OnMoveKeyPressed?.Invoke(PlayerInput.Movement);
    }
}
