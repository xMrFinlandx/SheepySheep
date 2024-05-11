using System;
using System.Collections.Generic;

namespace _Scripts.Utilities.StateMachine
{
    public class FiniteStateMachine
    {
        public FsmState CurrentState { get; private set; }
        public FsmState PreviousState { get; private set; }

        private readonly Dictionary<Type, FsmState> _states = new();

        public bool IsEqualsCurrentState<T>() where T : FsmState
        {
            return CurrentState.GetType() == typeof(T);
        }

        public void AddState(FsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (CurrentState != null && IsEqualsCurrentState<T>())
                return;

            if (!_states.TryGetValue(type, out var state))
                return;
            
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}