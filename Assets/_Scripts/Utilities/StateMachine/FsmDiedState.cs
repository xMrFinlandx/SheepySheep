using System;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmDiedState : FsmState
    {
        private Rigidbody2D _rigidbody;
        
        public static Action PlayerDiedAction;
        
        public FsmDiedState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody) : base(finiteStateMachine)
        {
            _rigidbody = rigidbody;
        }

        public override async void Enter()
        {
            Debug.Log("Player Died");
            _rigidbody.velocity = Vector2.zero;
            
            await Awaitable.WaitForSecondsAsync(1f);
            
            PlayerDiedAction?.Invoke();
        }
    }
}