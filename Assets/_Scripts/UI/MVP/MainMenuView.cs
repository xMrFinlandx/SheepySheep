using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.MVP
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

        private MainMenuPresenter _presenter;
        
        private void Start()
        {
            Init(new MainMenuPresenter(new MainMenuModel()));
        }

        private void Init(MainMenuPresenter presenter)
        {
            _presenter = presenter;
            
            _continueButton.onClick.AddListener(_presenter.OnContinueButtonClicked);
            _resetProgressButton.onClick.AddListener(_presenter.OnResetProgressButtonClicked);
            _selectLevelButton.onClick.AddListener(ShowLevelSelectionWindow);
            
            _mainLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction += _presenter.OnLevelSelected;
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
            
            _mainLevelsBinder.LevelSelectedAction -= _presenter.OnLevelSelected;
            _bonusLevelsBinder.LevelSelectedAction -= _presenter.OnLevelSelected;
        }
    }
}