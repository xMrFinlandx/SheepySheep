using _Scripts.Utilities.Classes;
using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.UI.MVVM
{
    public class MainMenuViewModel
    {
        protected readonly MainMenuModel MenuModel;
        
        public readonly ReactiveProperty<bool> IsContinueButtonPressed = new(false);
        public readonly ReactiveProperty<bool> IsDropProgressButtonPressed = new(false);
        
        public MainMenuViewModel(MainMenuModel menuModel)
        {
            MenuModel = menuModel;
        }

        public void OnContinueButtonClicked()
        {
            SceneManager.LoadSceneAsync(MenuModel.GetLastScene());
        }

        public void OnResetProgressButtonClicked()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }

        public void OnLevelSelected(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}