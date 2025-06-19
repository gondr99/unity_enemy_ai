using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] private Feedback[] feedbacks;

    public void PlayAllFeedbacks()
    {
        for (int i = 0; i < feedbacks.Length; i++)
        {
            feedbacks[i].PlayFeedback();
        }
    }
}
