using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Player
{
    public class TileSelector : MonoBehaviour
    {
        [SerializeField] private Transform _selectorTransform;

        private Vector2Int _currentCursorPosition;
        
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _currentCursorPosition = TilemapManager.Instance.WorldToCell( _camera.ScreenToWorldPoint(Input.mousePosition));

            if (TilemapManager.Instance.CanAddModifier(_currentCursorPosition))
                _selectorTransform.position = TilemapManager.Instance.CellToWorld(_currentCursorPosition);
        }
    }
}