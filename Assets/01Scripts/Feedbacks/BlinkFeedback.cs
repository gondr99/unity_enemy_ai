using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private float blinkDuration = 0.1f; 
    private readonly int _isBlinkHash = Shader.PropertyToID("_IsBlink");

    public override async void CreateFeedback()
    {
        targetRenderer.material.SetFloat(_isBlinkHash, 1f);
        await Awaitable.WaitForSecondsAsync(blinkDuration);
        FinishFeedback();
    }

    public override void FinishFeedback()
    {
        if(targetRenderer != null)
            targetRenderer.material.SetFloat(_isBlinkHash, 0f);
    }
}
