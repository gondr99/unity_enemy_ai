using UnityEngine;
using UnityEngine.Events;

public class Player : Agent
{
    [field:SerializeField] public InputReader PlayerInput {get; private set;}

    public UnityEvent<Vector2> OnMoveKeyPressed;

    private void Update()
    {
        UpdateMoveInput();
    }

    private void UpdateMoveInput()
    {
        OnMoveKeyPressed?.Invoke(PlayerInput.Movement);
    }
}
