using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFeedback : Feedback
{
    [SerializeField] private Light2D targetLight;
    [SerializeField] private float toggleTime = 0.1f;
    public override async void CreateFeedback()
    {
        targetLight.enabled = true;
        await Awaitable.WaitForSecondsAsync(toggleTime);
        FinishFeedback();
    }

    public override void FinishFeedback()
    {
        if(targetLight != null)
            targetLight.enabled = false;
    }
}
