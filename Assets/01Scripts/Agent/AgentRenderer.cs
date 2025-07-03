using UnityEngine;
using UnityEngine.Events;

public class AgentRenderer : MonoBehaviour, IAgentComponent
{
    public UnityEvent<bool> OnFlipEvent;
    public bool isFacingRight = true;
    protected Agent _owner;
    protected SpriteRenderer _spriteRenderer;

    
    protected int _agentLayerId;
    protected int _topLayerId;

    protected AgentMovement _movementCompo;

    [SerializeField] protected bool updateDirectionByMove = true;

    public virtual void Initialize(Agent owner)
    {
        _owner = owner;
        _agentLayerId = SortingLayer.NameToID("Agent");
        _topLayerId = SortingLayer.NameToID("Top");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementCompo = _owner.GetCompo<AgentMovement>();
    }

    protected virtual void Update()
    {
        if(updateDirectionByMove)
            UpdateVisual(_movementCompo.Velocity.x);
    }

    public virtual void UpdateVisual(float xValue)
    {
        bool isTurnLeft = isFacingRight && xValue < 0;
        bool isTurnRight = !isFacingRight && xValue > 0;
        if(isTurnLeft || isTurnRight ) 
            Flip();
    }

    public void SetSortingToTop(bool value)
    {
        _spriteRenderer.sortingLayerID = value ? _topLayerId : _agentLayerId;
    }

    #region Flip Section
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        float yAngle = isFacingRight ? 0 : 180f;
        
        _owner.transform.localEulerAngles = new Vector3(0, yAngle, 0);
        OnFlipEvent?.Invoke(isFacingRight);
    }
    #endregion
}
