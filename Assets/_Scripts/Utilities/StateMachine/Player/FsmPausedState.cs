using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmPausedState : FsmState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;
        
        public FsmPausedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, Animator animator) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
            _animator = animator;
        }

        public override void Enter()
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.speed = 0;
            _animator.enabled = false;
        }

        public override void Exit()
        {
            _animator.enabled = true;
            _animator.speed = 1;
        }
    }
}