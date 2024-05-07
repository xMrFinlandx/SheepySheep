using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmRunState : FsmMoveState
    {
        private readonly float _speed;
        private readonly float _speedModifier;
        private readonly int _animationHash;

        public FsmRunState(FiniteStateMachine finiteStateMachine, PlayerController playerController, Animator animator,
            int animationHash, float speed, float speedModifier) : base(finiteStateMachine, playerController,
            animator, animationHash, speed)
        {
            _speed = speedModifier * speed;
            _speedModifier = speedModifier;
            _animationHash = animationHash;
        }

        public override void Enter()
        {
            Animator.speed = _speedModifier;
            Animator.enabled = true;
            Animator.Play(_animationHash);
        }

        public override void FixedUpdate()
        {
            Rigidbody.velocity = PlayerController.MoveDirection * (_speed * Time.fixedDeltaTime);
        }
    }
}