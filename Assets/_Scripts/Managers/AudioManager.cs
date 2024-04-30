using System.Collections.Generic;
using System.Linq;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace _Scripts.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _audioSource;
        
        private ObjectPool<AudioSource> _pool;
        private readonly List<AudioSource> _audioSources = new();

        private void Start()
        {
            _pool = new ObjectPool<AudioSource>(CreateAudioSource);
        }

        private AudioSource CreateAudioSource() => Instantiate(_audioSource, transform);

        public void PlaySFX(AudioResource audioResource)
        {
            var freeSource = _audioSources.FirstOrDefault(source => !source.isPlaying);
            
            if (freeSource == null)
            {
                freeSource = _pool.Get();
                _audioSources.Add(freeSource);
            }

            freeSource.resource = audioResource;
            freeSource.Play();
        }
        
        public void PlayMusic(AudioResource audioResource)
        {
            
        }
    }
}