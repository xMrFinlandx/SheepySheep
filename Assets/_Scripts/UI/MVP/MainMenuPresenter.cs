using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.UI.MVP
{
    public class MainMenuPresenter
    {
        private readonly MainMenuModel _menuModel;

        public MainMenuPresenter(MainMenuModel menuModel)
        {
            _menuModel = menuModel;
        }

        public void OnContinueButtonClicked()
        {
             SceneManager.LoadScene(YandexGame.savesData.NextScene);
            
        }

        public void OnResetProgressButtonClicked()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }

        public void OnLevelSelected(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}