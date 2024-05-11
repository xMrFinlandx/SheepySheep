using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmIdleState : FsmState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Animator _animator;
        private readonly int _idleAnimation;

        public FsmIdleState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, Animator animator, int idleAnimation) : base(finiteStateMachine)
        {
            _spriteRenderer = spriteRenderer;
            _rigidbody = rigidbody;
            _animator = animator;
            _idleAnimation = idleAnimation;
        }

        public override void Enter()
        {
            _spriteRenderer.enabled = true;
            _animator.enabled = true;
            _animator.speed = 1;
            _rigidbody.velocity = Vector2.zero;
            
            FootstepsSoundManager.Instance.Stop();
            _animator.Play(_idleAnimation);
        }
    }
}