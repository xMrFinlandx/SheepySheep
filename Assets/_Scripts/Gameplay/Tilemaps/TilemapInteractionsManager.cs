using System;
using _Scripts.Gameplay.Tilemaps.Modifier;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps
{
    public class TilemapInteractionsManager : MonoBehaviour
    {
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Arrow _arrowPrefab;

        public static Action ArrowInstantiatedAction;
        
        private Camera _camera;

#if UNITY_EDITOR
        [Header("Editor only")]
        [SerializeField] private TilemapManager _tilemapManager;

        [Button("Move Interactions To Tile Center")]
        private void MoveToTileCenter()
        {
            var allTransforms = FindObjectsByType<Transform>(FindObjectsSortMode.None);
            
            foreach (var tr in allTransforms)
            {
                var tileModifier = tr.GetComponent<ITileModifier>();
                
                tileModifier?.MoveToCurrentTileCenter(_tilemapManager, tr);
            }
        }

        private void OnValidate()
        {
            _tilemapManager ??= FindAnyObjectByType<TilemapManager>();
        }
#endif

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
                ArrowInteract();

            if (Input.GetMouseButtonDown(1))
                RemoveArrow();
        }

        private void ArrowInteract()
        {
            var gridPos = GetGridMousePosition();

            if (!TilemapManager.Instance.CanAddModifier(gridPos))
            {
                Interact(gridPos);
                return;
            }
            
            ArrowInstantiatedAction?.Invoke();
            InstantiateArrow(gridPos);
        }

        private void RemoveArrow()
        {
            var gridPos = GetGridMousePosition();

            TilemapManager.Instance.TryRemoveInteraction(gridPos);
        }
        
        private static void Interact(Vector2Int gridPos)
        {
            TilemapManager.Instance.TryInteractInteraction(gridPos);
        }

        private void InstantiateArrow(Vector2Int gridPos)
        {
            var modifierPos = TilemapManager.Instance.CellToWorld(gridPos);
            var arrow = Instantiate(_arrowPrefab, modifierPos, Quaternion.identity);
            arrow.Init(_arrowConfig);
            
            TilemapManager.Instance.TryAddModifiers(gridPos, arrow);
        }

        private Vector2Int GetGridMousePosition()
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var gridPos = TilemapManager.Instance.WorldToCell(mousePos);
            return gridPos;
        }
    }
}