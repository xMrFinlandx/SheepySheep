using _Scripts.UI;
using YG;

namespace _Scripts.Utilities.StateMachine.Menu
{
    public class FsmSettingsWindow : FsmUIWindow
    {
        private readonly SettingsWindow _settingsWindow;
        
        public FsmSettingsWindow(IntFiniteStateMachine finiteStateMachine, SettingsWindow settingsWindow) : base(finiteStateMachine, settingsWindow.gameObject)
        {
            _settingsWindow = settingsWindow;
        }

        public override void Enter()
        {
            Show();
            _settingsWindow.Load();
        }

        public override void Exit()
        {
            _settingsWindow.Save();
            YandexGame.SaveProgress();
            Close();
        }
    }
}