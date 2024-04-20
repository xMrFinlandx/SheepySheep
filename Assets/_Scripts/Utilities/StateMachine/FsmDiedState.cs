using System;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmDiedState : FsmState
    {
        public static Action PlayerDiedAction;

        protected readonly float AwaitDuration;
        protected readonly Rigidbody2D Rigidbody;
        protected readonly SpriteRenderer SpriteRenderer;

        private readonly ParticleSystem _particleSystem;
        
        public FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, float awaitDuration) : base(finiteStateMachine)
        {
            SpriteRenderer = spriteRenderer;
            Rigidbody = rigidbody;
            AwaitDuration = awaitDuration;
        }

        public FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, ParticleSystem particleSystem, float awaitDuration) : base(finiteStateMachine)
        {
            SpriteRenderer = spriteRenderer;
            Rigidbody = rigidbody;
            AwaitDuration = awaitDuration;
            _particleSystem = particleSystem;
        }
        
        public override async void Enter()
        {
            Rigidbody.velocity = Vector2.zero;
            _particleSystem.Play();
            SpriteRenderer.enabled = false;
            
            await Awaitable.WaitForSecondsAsync(AwaitDuration);
            
            PlayerDiedAction?.Invoke();
        }

        public override void Exit()
        {
            SpriteRenderer.enabled = true;
        }
    }
}