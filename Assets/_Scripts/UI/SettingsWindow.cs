using System;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _masterSlider;

        public Action<float, BroAudioType> VolumeSliderChangedAction;
        
        private void Start()
        {
            _sfxSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.SFX));
            _musicSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.Music));
            _masterSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.All));
        }

        private void OnDestroy()
        {
            _sfxSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
            _masterSlider.onValueChanged.RemoveAllListeners();
        }
    }
}