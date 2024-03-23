using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    // TODO make class static
    public class SceneTransitionsManager : Singleton<SceneTransitionsManager>
    {
        public async void LoadScene(string sceneName)
        {
            await Awaitable.FromAsyncOperation(SceneManager.LoadSceneAsync(sceneName));
        }
    }
}