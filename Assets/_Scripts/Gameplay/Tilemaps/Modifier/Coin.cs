using _Scripts.Managers;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Interfaces;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Coin : BaseInteraction, IDictionarySave
    {
        [SerializeField] private string _guid;

        private bool _collected = false;
        
        public string Key => _guid;
        public bool Value => _collected;
        
        [Button("New Guid")]
        private void GenerateGUID()
        {
            _guid = System.Guid.NewGuid().ToString();
        }
        
        public override void Activate(IPlayerController playerController)
        {
            if (IsEnabled)
                return;

            IsEnabled = true;

            playerController.AddCoins(1);
            PlayCollectedAnimation();
        }
        
        private void Start()
        {
            InitializeSpawnPosition();
            
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