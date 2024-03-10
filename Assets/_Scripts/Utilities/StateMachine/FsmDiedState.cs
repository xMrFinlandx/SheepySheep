using System;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmDiedState : FsmState
    {
        public static Action PlayerDiedAction;
        
        public FsmDiedState(FiniteStateMachine finiteStateMachine) : base(finiteStateMachine)
        {
        }

        public override async void Enter()
        {
            Debug.Log("Player Died");

            await Awaitable.WaitForSecondsAsync(2f);
            
            PlayerDiedAction?.Invoke();
        }
    }
}