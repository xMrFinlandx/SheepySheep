using _Scripts.Managers;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Coin : BaseInteraction
    {
        public override void Activate(IPlayerController playerController)
        {
            if (IsEnabled)
                return;

            IsEnabled = true;
            
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

        private void OnValidate() => InitializeComponents();
    }
}