using System;
using System.Collections.Generic;

namespace _Scripts.Utilities.StateMachine
{
    public class FiniteStateMachine
    {
        private FsmState CurrentState;

        private Dictionary<Type, FsmState> _states = new();

        public void AddState(FsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
                return;

            if (!_states.TryGetValue(type, out var state))
                return;
            
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