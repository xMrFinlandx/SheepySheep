using YG;

namespace _Scripts.Utilities.Classes.MVVM
{
    public class MainMenuModel
    {
        public string GetLastScene()
        {
            return YandexGame.savesData.NextScene;
        }
    }
}