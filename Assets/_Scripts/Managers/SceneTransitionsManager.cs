using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public static class SceneTransitionsManager
    {
        public static async void LoadScene(string sceneName)
        {
            await Awaitable.FromAsyncOperation(SceneManager.LoadSceneAsync(sceneName));
        }
    }
}