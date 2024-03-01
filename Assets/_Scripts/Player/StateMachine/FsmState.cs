namespace _Scripts.Player.StateMachine
{
    public abstract class FsmState
    {
        protected readonly FiniteStateMachine FiniteStateMachine;

        public FsmState(FiniteStateMachine finiteStateMachine)
        {
            FiniteStateMachine = finiteStateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Update()
        {
        }
    }
}