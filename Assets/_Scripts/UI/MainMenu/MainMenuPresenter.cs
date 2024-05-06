using _Scripts.Player.Controls;
using Ami.BroAudio;
using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.UI.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly InputReader _inputReader;
        
        public MainMenuPresenter(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public void OnContinueButtonClicked()
        {
             SceneManager.LoadScene(YandexGame.savesData.NextScene);
             SetPlayerActions(true);
        }

        public void OnResetProgressButtonClicked()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }

        public void OnLevelSelected(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SetPlayerActions(true);
        }

        public void SetPlayerActions(bool isSetPlayer)
        {
            if (isSetPlayer)
            {
                _inputReader.SetGameplay();
            }
            else
            {
                _inputReader.SetUI();
            }
        }

        public void OnVolumeChanged(float volume, BroAudioType type)
        {
            BroAudio.SetVolume(type, volume);
        }
    }
}