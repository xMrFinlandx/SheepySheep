using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmRunState : FsmMoveState
    {
        private readonly float _speed;
        private readonly float _speedModifier;
        private readonly string _animationName;

        public FsmRunState(FiniteStateMachine finiteStateMachine, PlayerController playerController, Animator animator,
            string animationName, float speed, float speedModifier) : base(finiteStateMachine, playerController,
            animator, animationName, speed)
        {
            _speed = speedModifier * speed;
            _speedModifier = speedModifier;
            _animationName = animationName;
        }

        public override void Enter()
        {
            Animator.speed = _speedModifier;
            Animator.enabled = true;
            Animator.Play(_animationName);
        }

        public override void FixedUpdate()
        {
            Rigidbody.velocity = PlayerController.MoveDirection * (_speed * Time.fixedDeltaTime);
        }
    }
}