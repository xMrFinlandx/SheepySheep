using _Scripts.Managers;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Interfaces;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Coin : BaseInteraction, IDataPersistence
    {
        [SerializeField] private string _guid;

        private bool _isCollected = false;
        
        [Button("New Guid")]
        private void GenerateGUID()
        {
            _guid = System.Guid.NewGuid().ToString();
        }
        
        public override void Activate(IPlayerController playerController)
        {
            if (IsEnabled || _isCollected)
                return;

            IsEnabled = true;
            _isCollected = true;

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
            InitializeSpawnPosition();
            
            Animator.Play(AnimationName);
            ReloadRoomManager.ReloadRoomAction += Restart;

            YandexGame.savesData.collectables.TryGetValue(_guid, out bool collected);

            if (collected)
                Destroy(gameObject);
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

        public void SaveData()
        {
            if (YandexGame.savesData.collectables.ContainsKey(_guid)) 
                YandexGame.savesData.collectables.Remove(_guid);

            YandexGame.savesData.collectables.Add(_guid, _isCollected);
        }

        public void LoadData()
        {
            YandexGame.savesData.collectables.TryGetValue(_guid, out _isCollected);

            if (_isCollected) 
                gameObject.SetActive(false);
        }
    }
}