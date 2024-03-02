using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmPausedState : FsmState
    {
        public FsmPausedState(FiniteStateMachine finiteStateMachine) : base(finiteStateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("Activate pause");
        }
    }
}