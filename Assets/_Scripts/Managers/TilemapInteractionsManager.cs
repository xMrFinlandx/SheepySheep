using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Player.Controls;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Managers
{
    public class TilemapInteractionsManager : Singleton<TilemapInteractionsManager>
    {
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private DynamicArrow _dynamicArrowPrefab;
        
        public static Action ArrowInstantiatedAction;
        private InputReader _inputReader;

        private bool _canInteract = false;
        private bool _isInputAvailable = false;

#if UNITY_EDITOR
        [Space(20)]
        [Header("--- Editor only ---")]
        [SerializeField] private TilemapManager _tilemapManager;

        [Button("Move Interactions To Tile Center")]
        private void MoveToTileCenter()
        {
            var modifiers = Extensions.FindObjectsByInterface<ITileModifier>();
            
            foreach (var modifier in modifiers)
            {
                modifier.MoveToCurrentTileCenter(_tilemapManager);
            }
        }

        private void OnValidate()
        {
            _tilemapManager ??= FindAnyObjectByType<TilemapManager>();
        }
#endif
        
        [Inject]
        private void Construct([InjectOptional] InputReader inputReader)
        {
            _inputReader = inputReader;
            _isInputAvailable = inputReader != null;
        }
        
        public IList<SpriteRenderer> GetInteractionsSpriteRenderers()
        {
            return GetComponentsInChildren<SpriteRenderer>().Shuffle();
        }

        private void Start()
        {
            GameStateManager.GameStateChangedAction += OnGameStateChanged;

            if (!_isInputAvailable)
                return;
            
            _inputReader.LeftMouseClickEvent += ArrowInteract;
            _inputReader.RightMouseClickEvent += RemoveArrow;
        }

        private void OnDestroy()
        {
            GameStateManager.GameStateChangedAction -= OnGameStateChanged;
            
            if (!_isInputAvailable)
                return;
            
            _inputReader.LeftMouseClickEvent -= ArrowInteract;
            _inputReader.RightMouseClickEvent -= RemoveArrow;
        }

        private void OnGameStateChanged(GameStateType gameState)
        {
            _canInteract = gameState == GameStateType.Gameplay;
        }

        private void ArrowInteract(Vector2 mousePos)
        {
            if (!_canInteract)
                return;
            
            var gridPos = GetGridClickPosition(mousePos);
            
            if (!TilemapManager.Instance.CanAddModifier(gridPos))
            {
                if (TilemapManager.Instance.TryInteractInteraction(gridPos))
                    BroAudio.Play(_arrowConfig.RotationSound);
                 
                return;
            }
            
            ArrowInstantiatedAction?.Invoke();
            InstantiateArrow(gridPos);
        }

        private void RemoveArrow(Vector2 mousePos)
        {
            if (!_canInteract)
                return;
            
            var gridPos = GetGridClickPosition(mousePos);

            TilemapManager.Instance.TryRemoveInteraction(gridPos);
        }
        
        private void InstantiateArrow(Vector2Int gridPos)
        {
            var key = TilemapManager.Instance.CellToWorld(gridPos);
            var modifierPos = key - new Vector2(0, _arrowConfig.YOffset) + new Vector2(0, TilemapManager.Instance.YCellSize / 2);
            var arrow = Instantiate(_dynamicArrowPrefab, modifierPos, Quaternion.identity);
            
            arrow.Init(_arrowConfig, gridPos);
            
            BroAudio.Play(_arrowConfig.RotationSound);
            TilemapManager.Instance.TryAddModifiers(gridPos, arrow);
        }

        private static Vector2Int GetGridClickPosition(Vector2 mousePos)
        {
            var gridPos = TilemapManager.Instance.WorldToCell(mousePos);
            return gridPos;
        }
    }
}