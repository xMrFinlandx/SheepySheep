using YG;

namespace _Scripts.UI.MVVM
{
    public class MainMenuModel
    {
        public string GetLastScene()
        {
            return YandexGame.savesData.NextScene;
        }
    }
}