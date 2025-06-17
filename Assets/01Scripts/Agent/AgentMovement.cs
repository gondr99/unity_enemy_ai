using UnityEngine;

public class AgentMovement : MonoBehaviour, IAgentComponent
{
    public Rigidbody2D RigidCompo {get; private set;}
    public Vector2 Velocity => RigidCompo.linearVelocity;
    
    [SerializeField] private float _moveSpeed = 4f;
    private Agent _owner;
    private Vector2 _movementInput;

    public void Initialize(Agent owner)
    {
        _owner = owner;
        RigidCompo = GetComponent<Rigidbody2D>();
    }

    public void StopImmediately()
    {
        _movementInput = Vector2.zero;
        RigidCompo.linearVelocity = Vector2.zero;
    }

    public void SetMovement(Vector2 input)
    {
        _movementInput = input;
    }

    private void FixedUpdate()
    {
        RigidCompo.linearVelocity = _movementInput * _moveSpeed;
    }
}
