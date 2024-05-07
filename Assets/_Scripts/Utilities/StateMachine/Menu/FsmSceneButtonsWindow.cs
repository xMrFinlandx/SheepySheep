using _Scripts.UI;

namespace _Scripts.Utilities.StateMachine.Menu
{
    public class FsmSceneButtonsWindow : IntFsmState
    {
        private readonly SceneButtonsBinder _binder;
        
        public FsmSceneButtonsWindow(IntFiniteStateMachine finiteStateMachine, SceneButtonsBinder binder) : base(finiteStateMachine)
        {
            _binder = binder;
        }

        public override void Enter()
        {
            _binder.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            _binder.gameObject.SetActive(false);
        }
    }
}