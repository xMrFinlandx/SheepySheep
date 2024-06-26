﻿using _Scripts.Player.Controls;
using _Scripts.Utilities.StateMachine;
using _Scripts.Utilities.StateMachine.Menu;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using YG;
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
        [SerializeField] private PopupController _popupController;
        
        [Space] 
        [SerializeField] private GameObject _mainMenuButtons;
        [SerializeField] private SceneButtonsBinder _mainLevelsBinder;
        [SerializeField] private SceneButtonsBinder _bonusLevelsBinder;
        [SerializeField] private SettingsWindow _settingsWindow;

        private readonly IntFiniteStateMachine _finiteStateMachine = new();
        
        private MainMenuPresenter _presenter;
        private InputReader _inputReader;

        private const int _SETTINGS_WINDOW_INDEX = 0;
        private const int _MAIN_LEVELS_WINDOW_INDEX = 1;
        private const int _BONUS_LEVELS_WINDOW_INDEX = 2;
        private const int _NAVIGATION_WINDOW_INDEX = 3;
        
        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public void _ShowNavigationButtons() => ShowNavigationButtonsWindow();
        
        private void Start()
        {
            _presenter = new MainMenuPresenter(_inputReader);
            _presenter.SetPlayerActions(false);
            _presenter.LoadGameSettings();
            
            _inputReader.ResumeClickEvent += InputReaderOnResumeClickEvent;
            
            _continueButton.onClick.AddListener(_presenter.OnContinueButtonClicked);
            _resetProgressButton.onClick.AddListener(ShowPopup);
            _selectLevelButton.onClick.AddListener(ShowLevelSelectionWindow);
            _selectBonusLevelButton.onClick.AddListener(ShowBonusSelectionWindow);
            _settingsButton.onClick.AddListener(ShowSettingsWindow);
            _popupController.Hide();
            
            _mainLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
            _settingsWindow.VolumeSliderChangedAction += _presenter.OnVolumeChanged;
            
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _finiteStateMachine.Clear();
            
            _finiteStateMachine.AddState(new FsmSettingsWindow(_finiteStateMachine, _settingsWindow));
            _finiteStateMachine.AddState(new FsmSceneButtonsWindow(_finiteStateMachine, _mainLevelsBinder));
            _finiteStateMachine.AddState(new FsmSceneButtonsWindow(_finiteStateMachine, _bonusLevelsBinder));
            _finiteStateMachine.AddState(new FsmUIWindow(_finiteStateMachine, _mainMenuButtons));
            
            _finiteStateMachine.SetState(_NAVIGATION_WINDOW_INDEX);
        }
        
        [Button]
        private void ShowSettingsWindow() => SetState(_SETTINGS_WINDOW_INDEX);

        [Button]
        private void ShowBonusSelectionWindow() => SetState(_BONUS_LEVELS_WINDOW_INDEX);

        [Button]
        private void ShowNavigationButtonsWindow() => SetState(_NAVIGATION_WINDOW_INDEX);

        [Button]
        private void ShowLevelSelectionWindow() => SetState(_MAIN_LEVELS_WINDOW_INDEX);

        private void ShowPopup()
        {
            _continueButton.enabled = false;
            _selectLevelButton.enabled = false;
            _selectBonusLevelButton.enabled = false;
            _settingsButton.enabled = false;
            _resetProgressButton.enabled = false;

            _popupController.Show(_presenter.OnResetProgressButtonClicked, OnPopupCancelled, OnPopupCancelled);
        }

        private void OnPopupCancelled()
        {
            _popupController.Hide();
            
            _continueButton.enabled = true;
            _selectLevelButton.enabled = true;
            _selectBonusLevelButton.enabled = true;
            _settingsButton.enabled = true;
            _resetProgressButton.enabled = true;
        }

        private void SetState(int index)
        {
#if UNITY_EDITOR
            InitStateMachine();
#endif
            
            _finiteStateMachine.SetState(index);
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
            
            YandexGame.SaveProgress();
        }
    }
}