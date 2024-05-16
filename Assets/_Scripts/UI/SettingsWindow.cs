using System;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [Space] 
        [SerializeField] private Button _ruButton;
        [SerializeField] private Button _enButton;

        public Action<float, BroAudioType> VolumeSliderChangedAction;

        public void Save()
        {
            print("SAVE SFX");
            YandexGame.savesData.SFXVolume = _sfxSlider.value;
            YandexGame.SaveProgress();
        }

        public void Load()
        {
            print(YandexGame.savesData.SFXVolume);
            _sfxSlider.value = YandexGame.savesData.SFXVolume;
        }

        private void OnEnable()
        {
            _sfxSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.SFX));
            _ruButton.onClick.AddListener( () => ChangeLanguage("ru"));
            _enButton.onClick.AddListener( () => ChangeLanguage("en"));
        }

        private void ChangeLanguage(string language)
        {
            YandexGame.SwitchLanguage(language);
        }

        private void OnDisable()
        {
            _sfxSlider.onValueChanged.RemoveAllListeners();
            _ruButton.onClick.RemoveAllListeners();
            _enButton.onClick.RemoveAllListeners();
        }
    }
}