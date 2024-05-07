using System.Collections.Generic;

namespace _Scripts.Utilities.StateMachine
{
    public class IntFiniteStateMachine
    {
        public IntFsmState CurrentState { get; private set; }

        private readonly List<IntFsmState> _states = new();

        public void AddState(IntFsmState state)
        {
            _states.Add(state);
        }

        public bool IsEqualsCurrentState(int index)
        {
            return CurrentState == _states[index];
        }

        public void SetState(int index)
        {
            if (IsEqualsCurrentState(index))
                return;
            
            CurrentState?.Exit();
            CurrentState = _states[index];
            CurrentState.Enter();
        }

        public void Clear()
        {
            _states.Clear();
        }
    }
}