using UnityEngine;

namespace _Scripts.Managers
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() => Object.DontDestroyOnLoad( Object.Instantiate(Resources.Load("Prefabs/=== Persistent Managers ===")));
    }
}