using UnityEngine;

namespace _Scripts.Utilities
{
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            
            base.Awake();
        }
    }
}