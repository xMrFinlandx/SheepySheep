﻿using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

namespace _Scripts.Player
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private Transform _selectorTransform;

        private Vector2Int _currentCursorPosition;
        
        private Camera _camera;

        private void Start()
        {
            enabled = YandexGame.EnvironmentData.isDesktop;
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _currentCursorPosition = TilemapManager.Instance.WorldToCell( _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

            if (TilemapManager.Instance.CanAddModifier(_currentCursorPosition))
            {
                _selectorTransform.gameObject.SetActive(true);
                _selectorTransform.position = TilemapManager.Instance.CellToWorld(_currentCursorPosition);
            }
            else
            {
                _selectorTransform.gameObject.SetActive(false);
            }
        }
    }
}