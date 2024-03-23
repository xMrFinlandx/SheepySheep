using _Scripts.Managers;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class SpeedGem : BaseInteraction
    {
        [SerializeField] private bool _isIncreaseSpeed;

        public override void Activate(IPlayerController playerController)
        {
            if (IsEnabled)
                return;

            IsEnabled = true;
            
            if (_isIncreaseSpeed)
                playerController.SetState<FsmRunState>();
            else
                playerController.SetState<FsmMoveState>();

            PlayCollectedAnimation();
        }

        public override void Restart()
        {
            ResetProgress();
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