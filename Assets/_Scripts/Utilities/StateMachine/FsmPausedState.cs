using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmPausedState : FsmState
    {
        private Rigidbody2D _rigidbody;
        
        public FsmPausedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
        }

        public override void Enter()
        {
            Debug.Log("Activate pause");
            _rigidbody.velocity = Vector2.zero;
        }
    }
}