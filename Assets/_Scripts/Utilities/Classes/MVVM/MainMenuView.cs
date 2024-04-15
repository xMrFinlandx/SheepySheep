using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Utilities.Classes.MVVM
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Button _selectBonusLevelButton;
        [SerializeField] private Button _resetProgressButton;

        private MainMenuViewModel ViewModel;
        
        private void Start()
        {
            Init(new MainMenuViewModel(new MainMenuModel()));
        }

        public void Init(MainMenuViewModel viewModel)
        {
            ViewModel = viewModel;
            
            _continueButton.onClick.AddListener(ViewModel.OnContinueButtonClicked);
            _resetProgressButton.onClick.AddListener(ViewModel.OnResetProgressButtonClicked);
            
            viewModel.IsContinueButtonPressed.ValueChanged += OnContinueButtonPressed;
            viewModel.IsDropProgressButtonPressed.ValueChanged += OnDropProgressButtonPressed;
        }

        private void OnDropProgressButtonPressed(bool previous, bool current)
        {
            
        }

        private void OnContinueButtonPressed(bool previous, bool current)
        {
            
        }
    }
}