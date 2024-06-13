using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    protected Agent _owner;
    protected Animator _animator;

    public virtual void Initialize(Agent owner)
    {
        _owner = owner;    
        _animator = _owner.VisualTrm.GetComponent<Animator>();
    }

    public void SetBool(int hash, bool value)
    {
        _animator.SetBool(hash, value);
    }

    public void SetFloat(int hash, float value)
    {
        _animator.SetFloat(hash, value);
    }

    public void SetTrigger(int hash)
    {
        _animator.SetTrigger(hash);
    }
}
