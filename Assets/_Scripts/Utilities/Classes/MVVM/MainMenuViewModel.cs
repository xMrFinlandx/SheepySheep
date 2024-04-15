using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.Utilities.Classes.MVVM
{
    public class MainMenuViewModel
    {
        protected readonly MainMenuModel MenuModel;
        
        public readonly ReactiveProperty<bool> IsContinueButtonPressed = new(false);
        public readonly ReactiveProperty<bool> IsDropProgressButtonPressed = new(false);
        
        public ReactiveProperty<bool> ContinueButtonEnabled = new();
        public ReactiveProperty<bool> AdditionalLevelsButtonEnabled = new();
        public ReactiveProperty<bool> SelectLevelsButtonEnabled = new();
        public ReactiveProperty<bool> ResetProgressButtonEnabled = new();
        
        public MainMenuViewModel(MainMenuModel menuModel)
        {
            MenuModel = menuModel;
        }

        public void OnContinueButtonClicked()
        {
            ContinueButtonEnabled.Value = true;

            SceneManager.LoadSceneAsync(MenuModel.GetLastScene());
        }

        public void OnResetProgressButtonClicked()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
}