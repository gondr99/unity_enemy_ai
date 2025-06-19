using GondrLib.EventSystem;
using GondrLib.ObjectPool.RunTime;
using GondrLib.SoundSystem;
using UnityEngine;

public static class SoundEvents
{
    public static PlaySFXEvent PlaySfxEvent = new PlaySFXEvent();
}

public class PlaySFXEvent : GameEvent
{
    public SoundSO soundClip;
    public Vector3 position;

    public PlaySFXEvent Initializer(SoundSO clip, Vector3 playPosition)
    {
        soundClip = clip;
        position = playPosition;
        return this;
    }
}
