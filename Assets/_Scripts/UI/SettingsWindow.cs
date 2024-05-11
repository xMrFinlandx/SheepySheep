﻿using System;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;

        public Action<float, BroAudioType> VolumeSliderChangedAction;

        public void Save()
        {
            YandexGame.savesData.SFXVolume = _sfxSlider.value;
            YandexGame.savesData.MusicVolume = _musicSlider.value;
        }

        public void Load()
        {
            _sfxSlider.value = YandexGame.savesData.SFXVolume;
            _musicSlider.value = YandexGame.savesData.MusicVolume;
        }

        private void Start()
        {
            _sfxSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.SFX));
            _musicSlider.onValueChanged.AddListener(val => VolumeSliderChangedAction?.Invoke(val, BroAudioType.Music));
        }

        private void OnDestroy()
        {
            _sfxSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }
    }
}