using UnityEngine;

public class AgentRenderer : MonoBehaviour, IAgentComponent
{
    public bool isFacingRight = true;
    protected Agent _owner;
    protected SpriteRenderer _spriteRenderer;

    
    protected int _agentLayerId;
    protected int _topLayerId;

    protected AgentMovement _movementCompo;

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
        UpdateVisual();
    }

    protected virtual void UpdateVisual()
    {
        bool isTurnLeft = isFacingRight && _movementCompo.Velocity.x < 0;
        bool isTurnRight = !isFacingRight && _movementCompo.Velocity.x > 0;
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
    }
    #endregion
}
