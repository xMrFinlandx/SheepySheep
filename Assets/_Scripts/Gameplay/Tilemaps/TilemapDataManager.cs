using _Scripts.Gameplay.Tilemaps.Modifier;
using _Scripts.Managers;
using _Scripts.Scriptables;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps
{
    public class TilemapDataManager : MonoBehaviour
    {
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Arrow _arrowPrefab;
        
        private Camera _camera;
        
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
                RotateArrow(gridPos);
                return;
            }

            InstantiateArrow(gridPos);
        }

        private void RemoveArrow()
        {
            var gridPos = GetGridMousePosition();

            TilemapManager.Instance.TryRemoveInteraction(gridPos);
        }
        
        private static void RotateArrow(Vector2Int gridPos)
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