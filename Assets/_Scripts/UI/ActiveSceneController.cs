using System;
using _Scripts.Utilities.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public class ActiveSceneController : MonoBehaviour
    {
        [SerializeField] private SceneField _mainMenuScene;
        
        [SerializeField] private GameObject[] _gameplayObjects;
        [SerializeField] private GameObject[] _menuObjects;

        public static Action<bool> MenuSceneOpenedAction;
        
        private bool _isMenu = true;
        
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            _isMenu = _mainMenuScene == scene.name;
            MenuSceneOpenedAction?.Invoke(_isMenu);
            
            SetActive();
        }

        private void SetActive()
        {
            foreach (var gameplayObject in _gameplayObjects)
            {
                gameplayObject.SetActive(!_isMenu);
            }

            foreach (var menuObject in _menuObjects)
            {
                menuObject.SetActive(_isMenu);
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}