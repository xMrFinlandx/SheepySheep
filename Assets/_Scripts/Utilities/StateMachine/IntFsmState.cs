namespace _Scripts.Utilities.StateMachine
{
    public abstract class IntFsmState
    {
        private readonly IntFiniteStateMachine _finiteStateMachine;

        public IntFsmState(IntFiniteStateMachine finiteStateMachine)
        {
            _finiteStateMachine = finiteStateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}