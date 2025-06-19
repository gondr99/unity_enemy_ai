using System;
using GondrLib.EventSystem;
using GondrLib.ObjectPool.RunTime;
using GondrLib.SoundSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSo soundChannel;
    [SerializeField] private PoolManagerMono poolManager;

    [SerializeField] private PoolItemSO soundPlayerSo;
    private void Awake()
    {
        soundChannel.AddListener<PlaySFXEvent>(HandlePlaySFX);
    }

    private void OnDestroy()
    {
        soundChannel.RemoveListener<PlaySFXEvent>(HandlePlaySFX);
    }

    private void HandlePlaySFX(PlaySFXEvent evt)
    {
        SoundPlayer player = poolManager.Pop<SoundPlayer>(soundPlayerSo);
        player.transform.position = evt.position;
        player.PlaySound(evt.soundClip);
    }
}
