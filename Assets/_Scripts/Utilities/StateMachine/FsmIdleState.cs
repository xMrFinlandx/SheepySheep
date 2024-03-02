namespace _Scripts.Utilities.StateMachine
{
    public class FsmIdleState : FsmState
    {
        public FsmIdleState(FiniteStateMachine finiteStateMachine) : base(finiteStateMachine)
        {
        }

        public override void Exit()
        {
        }
    }
}