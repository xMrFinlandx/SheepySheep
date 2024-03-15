using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player.Controls
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions, GameControls.IUIActions
    {
        private GameControls _gameControls;
        private Camera _camera;

        public event Action<Vector2> LeftMouseClickEvent; 
        public event Action<Vector2> RightMouseClickEvent; 
        
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
        
        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            LeftMouseClickEvent?.Invoke(GetWorldMousePosition());
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            RightMouseClickEvent?.Invoke(GetWorldMousePosition());
        }
        
        public void OnPause(InputAction.CallbackContext context)
        {

        }

        public void OnSkipCutscene(InputAction.CallbackContext context)
        {
            
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnEnable()
        {
            if (_gameControls != null)
                return;
            
            _camera = Camera.main;
            _gameControls = new GameControls();
                
            _gameControls.Gameplay.SetCallbacks(this);
            _gameControls.UI.SetCallbacks(this);
                
            SetGameplay();
        }
        
        private Vector3 GetWorldMousePosition()
        {
            return _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}