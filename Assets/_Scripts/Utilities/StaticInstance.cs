using System;
using UnityEngine;

namespace _Scripts.Utilities
{
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public Action OnManagerDisabled;
        
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit()
        {
            OnManagerDisabled?.Invoke();
            Instance = null;
            Destroy(gameObject);
        }
    }
}