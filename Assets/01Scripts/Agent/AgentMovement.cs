using _01Scripts.BTEnemy.NinjaBT;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour, IAgentComponent
{
    [field: SerializeField] public Rigidbody2D RigidCompo {get; private set;}
    public Vector2 Velocity => RigidCompo.linearVelocity;
    [field:SerializeField] public AnimParamSo VelocityParam { get; private set; }
    public UnityEvent<int, float> OnSpeedParamChange;
    
    [SerializeField] private float moveSpeed = 4f;
    private Agent _owner;
    private Vector2 _movementInput;

    public bool CanManualMove { get; set; } = true;
    
    public void Initialize(Agent owner)
    {
        _owner = owner;
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
        if(CanManualMove)
            RigidCompo.linearVelocity = _movementInput * moveSpeed;

        if (VelocityParam != null)
        {
            float velocity = RigidCompo.linearVelocity.magnitude;
            OnSpeedParamChange?.Invoke(VelocityParam.hashValue, velocity);
        }
    }

    public void AddForceToAgent(Vector2 force)
    {
        RigidCompo.AddForce(force, ForceMode2D.Impulse);
    }
}
