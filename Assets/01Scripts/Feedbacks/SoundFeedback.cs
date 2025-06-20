using GondrLib.EventSystem;
using GondrLib.SoundSystem;
using UnityEngine;


public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO targetClip;
    [SerializeField] private GameEventChannelSo soundChannel;
    [SerializeField] private bool isBGM;
    public override void CreateFeedback()
    {
        if (isBGM == false)
        {
            var playSfxEvent = SoundEvents.PlaySfxEvent.Initializer(targetClip, transform.position);
            soundChannel.RaiseEvent(playSfxEvent);
        }
    }

    public override void FinishFeedback()
    {
        
    }
}
