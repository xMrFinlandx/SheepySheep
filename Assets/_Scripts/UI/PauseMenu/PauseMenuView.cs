using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Player.Controls;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.UI.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private InputReader _inputReader;
        
        private PauseMenuPresenter _presenter;

        private bool _isTeleportEnabled;

        private bool _isDisabled => GameStateManager.CurrentGameState == GameStateType.Cutscene ||
                                    GameStateManager.CurrentGameState == GameStateType.Unset ||
                                    _isTeleportEnabled;

        private void Start()
        {
            _presenter = new PauseMenuPresenter(_inputReader);

            Teleport.TeleportEnabledAction += OnTeleportEnabled;
            
            _inputReader.PauseClickEvent += OnPause;
            _inputReader.ResumeClickEvent += OnResume;
            
            _presenter.OnResume(false);
            _background.SetActive(false);
        }

        private void OnTeleportEnabled(bool isEnabled)
        {
            _isTeleportEnabled = isEnabled;
        }

        private void OnResume()
        {
            if (_isDisabled)
                return;

            _presenter.OnResume();
            _background.SetActive(false);
        }

        private void OnPause()
        {
            if (_isDisabled)
                return;
            
            _presenter.OnPause();
            _background.SetActive(true);
        }

        private void OnDestroy()
        {
            _inputReader.PauseClickEvent -= OnPause;
            _inputReader.ResumeClickEvent -= OnResume;

            Teleport.TeleportEnabledAction -= OnTeleportEnabled;
        }
    }
}