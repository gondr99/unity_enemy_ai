using GondrLib.ObjectPool.RunTime;
using UnityEngine;
using UnityEngine.Audio;

namespace GondrLib.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioMixerGroup sfxGroup;
        [SerializeField] private AudioMixerGroup musicGroup;
        [SerializeField] private AudioSource _audioSource;
        public GameObject GameObject => gameObject;
        [field:SerializeField] public PoolItemSO PoolItem { get; private set; }
        private Pool _myPool;
        
        public void SetUpPool(Pool pool) => _myPool = pool;

        public void ResetItem()
        {
            //do nothing
        }

        public void PlaySound(SoundSO data)
        {

            if (data.audioType == SoundSO.AudioTypes.SFX)
            {
                _audioSource.outputAudioMixerGroup = sfxGroup;
            }
            else if (data.audioType == SoundSO.AudioTypes.Music)
            {
                _audioSource.outputAudioMixerGroup = musicGroup;
            }

            _audioSource.volume = data.volume;
            _audioSource.pitch = data.pitch;
            if (data.randomizePitch)
            {
                _audioSource.pitch += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
            }
            _audioSource.clip = data.clip;

            _audioSource.loop = data.loop;

            if (!data.loop)
            {
                float time = _audioSource.clip.length + .2f;
                DisableSoundTimer(time);
            }
            _audioSource.Play();
        }

        private async void DisableSoundTimer(float time)
        {
            await Awaitable.WaitForSecondsAsync(time);
            _myPool.Push(this);
        }

        public void StopAndGoToPool()
        {
            _audioSource.Stop();
            _myPool.Push(this);
        }
    }
}