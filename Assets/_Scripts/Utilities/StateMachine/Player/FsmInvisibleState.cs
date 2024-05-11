using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmInvisibleState : FsmState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer _spriteRenderer;
        
        public FsmInvisibleState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
            _spriteRenderer = spriteRenderer;
        }

        public override void Enter()
        {
            _spriteRenderer.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            FootstepsSoundManager.Instance.Stop();
        }
    }
}