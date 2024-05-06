using _Scripts.Player.Controls;
using Ami.BroAudio;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Button _selectBonusLevelButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _resetProgressButton;
        [Space] 
        [SerializeField] private GameObject _mainMenuButtons;
        [SerializeField] private SceneButtonsBinder _mainLevelsBinder;
        [SerializeField] private SceneButtonsBinder _bonusLevelsBinder;
        [SerializeField] private SettingsWindow _settingsWindow;

        [Header("Test")] 
        [SerializeField] private SoundID _soundID;

        private MainMenuPresenter _presenter;
        private InputReader _inputReader;
        
        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }
        
        private void Start()
        {
            _presenter = new MainMenuPresenter(_inputReader);
            _presenter.SetPlayerActions(false);
            
            _inputReader.ResumeClickEvent += InputReaderOnResumeClickEvent;
            
            _continueButton.onClick.AddListener(_presenter.OnContinueButtonClicked);
            _resetProgressButton.onClick.AddListener(_presenter.OnResetProgressButtonClicked);
            _selectLevelButton.onClick.AddListener(ShowLevelSelectionWindow);
            _selectBonusLevelButton.onClick.AddListener(ShowBonusSelectionWindow);
            _settingsButton.onClick.AddListener(ShowSettingsWindow);
            
            _mainLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
            _settingsWindow.VolumeSliderChangedAction += _presenter.OnVolumeChanged;

            InvokeRepeating(nameof(test_call), 5, 1f);
        }

        private void test_call()
        {
            BroAudio.Play(_soundID);
        }

        [Button]
        private void ShowSettingsWindow()
        {
            _settingsWindow.gameObject.SetActive(true);
            
            _mainMenuButtons.gameObject.SetActive(false);
            _mainLevelsBinder.gameObject.SetActive(false);
            _bonusLevelsBinder.gameObject.SetActive(false);
        }

        [Button]
        private void ShowBonusSelectionWindow()
        {
            
        }

        [Button]
        private void ShowNavigationButtonsWindow()
        {
            _mainMenuButtons.gameObject.SetActive(true);
            
            _mainLevelsBinder.gameObject.SetActive(false);
            _bonusLevelsBinder.gameObject.SetActive(false);
            _settingsWindow.gameObject.SetActive(false);
        }

        [Button]
        private void ShowLevelSelectionWindow()
        {
            _mainLevelsBinder.gameObject.SetActive(true);
            
            _mainMenuButtons.gameObject.SetActive(false);
            _bonusLevelsBinder.gameObject.SetActive(false);
            _settingsWindow.gameObject.SetActive(false);
        }

        private void InputReaderOnResumeClickEvent()
        {
            ShowNavigationButtonsWindow();
        }
        
        private void OnDestroy()
        {
            _inputReader.ResumeClickEvent -= InputReaderOnResumeClickEvent;
            
            _continueButton.onClick.RemoveAllListeners();
            _resetProgressButton.onClick.RemoveAllListeners();
            _selectLevelButton.onClick.RemoveAllListeners();
            _selectBonusLevelButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            
            _mainLevelsBinder.LevelSelectedAction -= _presenter.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction -= _presenter.OnLevelSelected;
            _settingsWindow.VolumeSliderChangedAction -= _presenter.OnVolumeChanged;
        }
    }
}