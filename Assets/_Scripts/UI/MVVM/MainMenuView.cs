using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.MVVM
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

        private MainMenuViewModel ViewModel;
        
        private void Start()
        {
            Init(new MainMenuViewModel(new MainMenuModel()));
        }

        private void Init(MainMenuViewModel viewModel)
        {
            ViewModel = viewModel;
            
            _continueButton.onClick.AddListener(ViewModel.OnContinueButtonClicked);
            _resetProgressButton.onClick.AddListener(ViewModel.OnResetProgressButtonClicked);
            _selectLevelButton.onClick.AddListener(ShowLevelSelectionWindow);
            
            ViewModel.IsContinueButtonPressed.ValueChanged += OnContinueButtonPressed;
            ViewModel.IsDropProgressButtonPressed.ValueChanged += OnDropProgressButtonPressed;
            
            _mainLevelsBinder.LevelSelectedAction += ViewModel.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction += ViewModel.OnLevelSelected;
        }

        [Button]
        private void ShowNavigationButtonsWindow()
        {
            _mainMenuButtons.gameObject.SetActive(true);
            
            _mainLevelsBinder.gameObject.SetActive(false);
            _bonusLevelsBinder.gameObject.SetActive(false);
        }

        [Button]
        private void ShowLevelSelectionWindow()
        {
            _mainLevelsBinder.gameObject.SetActive(true);
            
            _mainMenuButtons.gameObject.SetActive(false);
            _bonusLevelsBinder.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _resetProgressButton.onClick.RemoveAllListeners();
            _selectLevelButton.onClick.RemoveAllListeners();
            _selectBonusLevelButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            
            ViewModel.IsContinueButtonPressed.ValueChanged -= OnContinueButtonPressed;
            ViewModel.IsDropProgressButtonPressed.ValueChanged -= OnDropProgressButtonPressed;
            
            _mainLevelsBinder.LevelSelectedAction -= ViewModel.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction -= ViewModel.OnLevelSelected;
        }

        private void OnDropProgressButtonPressed(bool previous, bool current)
        {
        }

        private void OnContinueButtonPressed(bool previous, bool current)
        {
        }
        
        
    }
}