using UnityEngine;

namespace _Scripts.Player.StateMachine
{
    public class FsmIdleState : FsmState
    {
        public FsmIdleState(FiniteStateMachine finiteStateMachine) : base(finiteStateMachine)
        {
        }

        public override void Exit()
        {
            Debug.Log("Player Start Move");
        }
    }
}