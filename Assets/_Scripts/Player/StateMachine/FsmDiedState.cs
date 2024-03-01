using UnityEngine;

namespace _Scripts.Player.StateMachine
{
    public class FsmDiedState : FsmState
    {
        public FsmDiedState(FiniteStateMachine finiteStateMachine) : base(finiteStateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("Player Died");
        }
    }
}