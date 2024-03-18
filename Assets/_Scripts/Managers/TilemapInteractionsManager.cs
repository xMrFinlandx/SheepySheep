using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps.Modifier;
using _Scripts.Player.Controls;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Managers
{
    public class TilemapInteractionsManager : Singleton<TilemapInteractionsManager>
    {
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private DynamicArrow _dynamicArrowPrefab;
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        
        public static Action ArrowInstantiatedAction;
        private InputReader _inputReader;

        private bool _canInteract = false;

        public IReadOnlyList<SpriteRenderer> SpriteRenderers => (IReadOnlyList<SpriteRenderer>) _spriteRenderers.Shuffle();

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

        [Button("Get Interactions")]
        private void FillInteractions()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void OnValidate()
        {
            _tilemapManager ??= FindAnyObjectByType<TilemapManager>();
        }
#endif

        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void Start()
        {
            GameStateManager.GameStateChangedAction += OnGameStateChanged;
            _inputReader.LeftMouseClickEvent += ArrowInteract;
            _inputReader.RightMouseClickEvent += RemoveArrow;
        }

        private void OnDestroy()
        {
            GameStateManager.GameStateChangedAction -= OnGameStateChanged;
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
            
            var gridPos = GetGridMousePosition(mousePos);

            if (!TilemapManager.Instance.CanAddModifier(gridPos))
            {
                Interact(gridPos);
                return;
            }
            
            ArrowInstantiatedAction?.Invoke();
            InstantiateArrow(gridPos);
        }

        private void RemoveArrow(Vector2 mousePos)
        {
            if (!_canInteract)
                return;
            
            var gridPos = GetGridMousePosition(mousePos);

            TilemapManager.Instance.TryRemoveInteraction(gridPos);
        }
        
        private void InstantiateArrow(Vector2Int gridPos)
        {
            var modifierPos = TilemapManager.Instance.CellToWorld(gridPos);
            var arrow = Instantiate(_dynamicArrowPrefab, modifierPos, Quaternion.identity);
            arrow.Init(_arrowConfig);
            
            TilemapManager.Instance.TryAddModifiers(gridPos, arrow);
        }
        
        private static void Interact(Vector2Int gridPos)
        {
            TilemapManager.Instance.TryInteractInteraction(gridPos);
        }

        private static Vector2Int GetGridMousePosition(Vector2 mousePos)
        {
            var gridPos = TilemapManager.Instance.WorldToCell(mousePos);
            return gridPos;
        }
    }
}