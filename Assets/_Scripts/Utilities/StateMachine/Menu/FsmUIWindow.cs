using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Menu
{
    public class FsmUIWindow : IntFsmState
    {
        private readonly GameObject _gameObject;

        public FsmUIWindow(IntFiniteStateMachine finiteStateMachine, GameObject gameObject) : base(finiteStateMachine)
        {
            _gameObject = gameObject;
        }

        protected void Show() => _gameObject.SetActive(true);

        protected void Close() => _gameObject.SetActive(false);

        public override void Enter() => Show();

        public override void Exit() => Close();
    }
}