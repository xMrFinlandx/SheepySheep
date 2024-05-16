using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Managers;
using _Scripts.Player.Controls;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private InputReader _inputReader;
        [Space]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _continueButton;
        [Space] 
        [SerializeField] private GameObject _navigationButtons;
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private SceneField _mainMenuScene;
        
        private PauseMenuPresenter _presenter;

        private bool _isTeleportEnabled;

        private bool _isDisabled => GameStateManager.CurrentGameState == GameStateType.Cutscene ||
                                    GameStateManager.CurrentGameState == GameStateType.Unset ||
                                    _isTeleportEnabled;

        public void _ShowNavigationButtons() => ShowNavigationButtons();
        
        private void Start()
        {
            _presenter = new PauseMenuPresenter(_inputReader);

            Teleport.TeleportEnabledAction += OnTeleportEnabled;
            
            _inputReader.PauseClickEvent += OnPause;
            _inputReader.ResumeClickEvent += OnResume;
            
            _settingsButton.onClick.AddListener(ShowSettings);
            _menuButton.onClick.AddListener(OpenMainMenu);
            _continueButton.onClick.AddListener(OnResume);
            
            _settingsWindow.VolumeSliderChangedAction += _presenter.OnVolumeChanged;
            
            _presenter.OnResume(false);
            _background.SetActive(false);
            _settingsWindow.gameObject.SetActive(false);
            CloseNavigationButtons();
        }
        
        private void OpenMainMenu()
        {
            BroAudio.Stop(BroAudioType.All);
            FootstepsSoundManager.Instance.Stop();
            OnResume();
            SceneManager.LoadScene(_mainMenuScene);
        }

        private void ShowNavigationButtons(bool closeSettings = true)
        {
            _navigationButtons.SetActive(true);

            if (closeSettings)
                CloseSettings();
        }

        private void CloseNavigationButtons()
        {
            _navigationButtons.SetActive(false);
        }

        private void ShowSettings()
        {
            CloseNavigationButtons();
            
            _settingsWindow.gameObject.SetActive(true);
            _settingsWindow.Load();
        }

        private void CloseSettings()
        {
            _settingsWindow.Save();
            _settingsWindow.gameObject.SetActive(false);
        }

        private void OnTeleportEnabled(bool isEnabled)
        {
            _isTeleportEnabled = isEnabled;
        }

        private void OnResume()
        {
            if (_isDisabled)
                return;

            CloseNavigationButtons();
            CloseSettings();
            _presenter.OnResume();
            _background.SetActive(false);
        }

        private void OnPause()
        {
            if (_isDisabled)
                return;
            
            _presenter.OnPause();
            _background.SetActive(true);
            ShowNavigationButtons(false);
        }

        private void OnDestroy()
        {
            _inputReader.PauseClickEvent -= OnPause;
            _inputReader.ResumeClickEvent -= OnResume;
            
            if (_settingsWindow != null && _presenter != null)
                _settingsWindow.VolumeSliderChangedAction -= _presenter.OnVolumeChanged;
            
            _settingsButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();

            Teleport.TeleportEnabledAction -= OnTeleportEnabled;
        }
    }
}