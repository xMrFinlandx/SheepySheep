using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.UI;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmDiedState : FsmState
    {
        protected readonly Rigidbody2D Rigidbody;
        protected readonly SpriteRenderer SpriteRenderer;
        
        private readonly ParticleSystem _particleSystem;
        private readonly float AwaitDuration;
        
        private const float _FADE_DELAY = .5f;
        
        public FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, ParticleSystem particleSystem, float awaitDuration) : base(finiteStateMachine)
        {
            SpriteRenderer = spriteRenderer;
            Rigidbody = rigidbody;
            AwaitDuration = awaitDuration;
            _particleSystem = particleSystem;
        }
        
        protected FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, float awaitDuration) : base(finiteStateMachine)
        {
            SpriteRenderer = spriteRenderer;
            Rigidbody = rigidbody;
            AwaitDuration = awaitDuration;
        }

        public override async void Enter()
        {
            Rigidbody.velocity = Vector2.zero;
            _particleSystem.Play();
            SpriteRenderer.enabled = false;
            FootstepsSoundManager.Instance.Stop();
            GameStateManager.SetState(GameStateType.Cutscene);
            
            await Awaitable.WaitForSecondsAsync(AwaitDuration);

            Kill();
        }

        protected static void Kill()
        {
            Fader.Instance.Show(() =>
                {
                    PlayerController.PlayerDiedAction?.Invoke();
                }, () => 
                    !AdAfterDeathManager.Instance.TryShowAd(),
                _FADE_DELAY);
        }

        public override void Exit()
        {
            SpriteRenderer.enabled = true;
            /*Fader.Instance.Hide();*/
            GameStateManager.SetState(GameStateType.Gameplay);
        }
    }
}