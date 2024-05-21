using System;
using _Scripts.Utilities.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.UI
{
    public class ActiveSceneController : MonoBehaviour
    {
        [SerializeField] private SceneField _mainMenuScene;
        
        [SerializeField] private GameObject[] _gameplayObjects;
        [SerializeField] private GameObject[] _menuObjects;
        [SerializeField] private GameObject[] _mobileObjects;
        [SerializeField] private GameObject[] _desktopObjects;

        public static Action<bool> MenuSceneOpenedAction;
        
        private bool _isMenu = true;
        private bool _isDesktop;
        
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            YandexGame.GetDataEvent += OnDataEvent;
        }

        private void OnDataEvent()
        {
            _isDesktop = YandexGame.EnvironmentData.isDesktop;
            SetActive();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            _isMenu = _mainMenuScene == scene.name;
            _isDesktop = YandexGame.SDKEnabled && YandexGame.EnvironmentData.isDesktop;
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

            foreach (var desktopObject in _desktopObjects)
            {
                desktopObject.SetActive(_isDesktop);
            }

            foreach (var mobileObject in _mobileObjects)
            {
                mobileObject.SetActive(!_isDesktop);
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            YandexGame.GetDataEvent -= OnDataEvent;
        }
    }
}