using UnityEngine;
public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    public virtual void PlayFeedback()
    {
        FinishFeedback();
        CreateFeedback();
    }
}
