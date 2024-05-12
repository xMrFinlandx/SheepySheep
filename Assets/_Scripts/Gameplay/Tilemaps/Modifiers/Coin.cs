using System;
using _Scripts.Managers;
using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Coin : BaseCollectable, IDataPersistence
    {
        [SerializeField] private string _guid;

        private bool _isCollected = false;
        
        [Button("New Guid")]
        private void GenerateGUID()
        {
            _guid = Guid.NewGuid().ToString();
        }
        
        public void SaveData()
        {
            YandexGame.savesData.AddCollectable(_guid, _isCollected);
        }

        public void LoadData()
        {
            _isCollected = YandexGame.savesData.IsCollectableEnabled(_guid);
            
            if (_isCollected) 
                gameObject.SetActive(false);
        }
        
        public override void Activate(IPlayerController playerController)
        {
            if (IsEnabled || _isCollected)
                return;

            IsEnabled = true;
            _isCollected = true;
            BroAudio.Play(Config.CollectedSound);

            playerController.AddCoins(1);
            PlayCollectedAnimation();
        }

        public override void Restart()
        {
            ResetProgress();
            
            if (!IsEnabled)
                _isCollected = false;
        }

        private void Start()
        {
            Animator.Play(AnimationName);
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_guid))
                GenerateGUID();
            
            InitializeComponents();
        }
    }
}