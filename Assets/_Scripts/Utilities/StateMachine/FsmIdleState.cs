using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmIdleState : FsmState
    {
        private Rigidbody2D _rigidbody;
        
        public FsmIdleState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
        }

        public override void Enter()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}