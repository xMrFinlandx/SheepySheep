using System;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmDiedState : FsmState
    {
        private readonly int _animationHash;
        private readonly float _fallDuration;
        private readonly float _gravityScale;
        
        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;
        
        public static Action PlayerDiedAction;
        
        public FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody,
            Animator animator, int deathAnimation, float fallDuration, float gravityScale) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
            _animator = animator;
            _animationHash = deathAnimation;
            _fallDuration = fallDuration;
            _gravityScale = gravityScale;
        }

        public override async void Enter()
        {
            _rigidbody.gravityScale = _gravityScale;
            
            _rigidbody.velocity = Vector2.zero;
            _animator.Play(_animationHash);
            
            await Awaitable.WaitForSecondsAsync(_fallDuration);
            
            PlayerDiedAction?.Invoke();
        }

        public override void Exit()
        {
            _rigidbody.gravityScale = 0f;
        }
    }
}