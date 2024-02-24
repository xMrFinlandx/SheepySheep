using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Gameplay.Tilemaps
{
    public class TilemapDataManager : MonoBehaviour
    {
        private Tilemap _tilemap;
        private Camera _camera;

        [Inject]
        private void Construct(Tilemap tilemap) => _tilemap = tilemap;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) 
                return;
            
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var gridPos = _tilemap.WorldToCell(mousePos);

            if (!_tilemap.HasTile(gridPos))
                return;

            var selectedTile = _tilemap.GetTile(gridPos);

            print($"at pos {gridPos}, selected tile {selectedTile}");
        }
    }
}