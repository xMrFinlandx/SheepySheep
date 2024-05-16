using System;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

namespace _Scripts.Player.Controls
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions, GameControls.IUIActions
    {
        private GameControls _gameControls;
        private Camera _camera;

        private bool _isAdditiveMode = true;

        private bool _isDesktop => YandexGame.EnvironmentData.isDesktop;

        public event Action<bool> SetAdditiveModeEvent; 
        
        public event Action<Vector2> LeftMouseClickEvent; 
        public event Action<Vector2> RightMouseClickEvent;

        public event Action PauseClickEvent;
        public event Action ResumeClickEvent;
        
        public void Disable()
        {
            _gameControls.Gameplay.Disable();
            _gameControls.UI.Disable();
        }

        public void SetGameplay()
        {
            _gameControls.Gameplay.Enable();
            _gameControls.UI.Disable();
        }

        public void SetUI()
        {
            _gameControls.Gameplay.Disable();
            _gameControls.UI.Enable();
        }

        public void SetAdditive()
        {
            if (_isDesktop)
                return;
            
            SetAdditiveModeEvent?.Invoke(true);
            _isAdditiveMode = true;
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;
            
            if (_isDesktop)
            {
                LeftMouseClickEvent?.Invoke(GetWorldMousePosition(_gameControls.Gameplay.TouchPosition));
            }
            else
            {
                if (_isAdditiveMode)
                {
                    LeftMouseClickEvent?.Invoke(GetWorldMousePosition(_gameControls.Gameplay.TouchPosition));
                }
                else
                {
                    RightMouseClickEvent?.Invoke(GetWorldMousePosition(_gameControls.Gameplay.TouchPosition));
                }
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed || !_isDesktop)
                return;

            RightMouseClickEvent?.Invoke(GetWorldMousePosition(_gameControls.Gameplay.TouchPosition));
        }
        
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;
            
            PauseClickEvent?.Invoke();
        }
        
        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;
            
            ResumeClickEvent?.Invoke();
        }

        public void OnSkipCutscene(InputAction.CallbackContext context)
        {
        }

        public void OnTouchPosition(InputAction.CallbackContext context)
        {
        }

        public void OnSetAdditiveMode(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed || _isDesktop)
                return;

            SetAdditive();
        }

        public void OnSetRemoveMode(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed || _isDesktop)
                return;

            SetAdditiveModeEvent?.Invoke(false);
            _isAdditiveMode = false;
        }

        public void Init(Camera mainCamera)
        {
            _camera = mainCamera;
        }

        private void OnEnable()
        {
            if (_gameControls != null && _camera != null)
                return;
            
            _gameControls = new GameControls();
            _camera = Camera.main;    
            
            _gameControls.Gameplay.SetCallbacks(this);
            _gameControls.UI.SetCallbacks(this);
                
            SetGameplay();
        }
        
        private Vector3 GetWorldMousePosition(InputAction context)
        {
            return _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }
}