using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmIdleState : FsmState
    {
        private readonly Rigidbody2D _rigidbody;
        
        public FsmIdleState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
        }

        public override void Enter()
        {
            _rigidbody.velocity = Vector2.zero;
            FootstepsSoundManager.Instance.Stop();
        }
    }
}