using _Scripts.Player.Controls;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;

namespace _Scripts.UI.PauseMenu
{
    public class PauseMenuPresenter
    {
        private readonly InputReader _inputReader;

        public PauseMenuPresenter(InputReader inputReader)
        {
            _inputReader = inputReader;
        }
        
        public void OnResume(bool setGameplayState = true)
        {
            _inputReader.SetGameplay();

            if (setGameplayState)
                GameStateManager.SetState(GameStateType.Gameplay);
        }

        public void OnPause()
        {
            _inputReader.SetUI();
            GameStateManager.SetState(GameStateType.Paused);
        }
    }
}