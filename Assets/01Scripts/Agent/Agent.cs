using UnityEngine;

[RequireComponent(typeof(AgentMovement), typeof(AgentRenderer))]
public abstract class Agent : MonoBehaviour
{
    public AgentMovement MoveCompo {get; protected set;}
    public AgentAnimator AnimatorCompo {get; protected set;}
    public AgentRenderer RenderCompo {get; protected set;}    
    public Transform VisualTrm {get; protected set;}
    

    protected virtual void Awake()
    {
        VisualTrm = transform.Find("Visual");
        MoveCompo = GetComponent<AgentMovement>();
        MoveCompo.Initialize(this);

        AnimatorCompo = GetComponent<AgentAnimator>();
        AnimatorCompo.Initialize(this);

        RenderCompo = GetComponent<AgentRenderer>();
        RenderCompo.Initialize(this);
    }

    
}
