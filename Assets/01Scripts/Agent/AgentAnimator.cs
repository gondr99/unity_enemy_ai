using UnityEngine;

public class AgentAnimator : MonoBehaviour, IAgentComponent
{
    protected Agent _owner;
    protected Animator _animator;

    private int _currentClipHash;

    public void ChangeClip(int nextHash)
    {
        if (_currentClipHash != 0)
        {
            SetBool(_currentClipHash, false);
        }

        _currentClipHash = nextHash;
        SetBool(_currentClipHash, true);
    }
    
    public virtual void Initialize(Agent owner)
    {
        _owner = owner;    
        _animator = GetComponent<Animator>();
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
