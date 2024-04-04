using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using TileData = _Scripts.Gameplay.Tilemaps.TileData;

namespace _Scripts.Utilities.Tools
{
    public class TilemapOutliner : MonoBehaviour
    {
#if UNITY_EDITOR
        
        [SerializeField] private Tilemap _sourceTilemap;
        [SerializeField] private Tilemap _outline;
        [Space] 
        [SerializeField] private Vector3Int _offset = new(-1, -1);
        [Space] 
        [SerializeField] private bool _fillInner = false;
        [ShowIf(nameof(_fillInner))]
        [SerializeField] private TileBase _innerFillTile;
        [FormerlySerializedAs("_tileData")]
        [Space]
        [Expandable]
        [SerializeField] private TilesContainer _tilesContainer;
        [FormerlySerializedAs("_diagonalTileData")]
        [Expandable]
        [SerializeField] private TilesContainer _diagonalTilesContainer;

        [Space] [SerializeField] private List<TileData> _tileData = null;

        private Dictionary<TileBase, TileData> _dataFromTiles = new();

        private Vector2Int[] _tilePositions;

        [Button("Toggle Source Tilemap")]
        private void ToggleSource()
        {
            Undo.RecordObject(_sourceTilemap.gameObject, "Source Tilemap Toggle");
            
            _sourceTilemap.gameObject.SetActive(!_sourceTilemap.gameObject.activeInHierarchy);
        }

        [Button("Clear Outline")]
        private void ClearOutline()
        {
            Undo.RecordObject(_outline, "Outline Cleared");

            _dataFromTiles.Clear();
            _tilePositions = null;
            _outline.ClearAllTiles();
        }

        [Button("Generate Outline")]
        private void GenerateOutline()
        {
            Undo.RecordObject(_outline, "Outline Changed");

            ClearOutline();
            FillTileDataDictionary();
            
            _tilePositions = GetAllTilePositions(_sourceTilemap);
            
            GenerateTiles(_diagonalTilesContainer.TilemapOffsetData, _tilePositions);
            GenerateTiles(_tilesContainer.TilemapOffsetData, _tilePositions);
            
            if (_fillInner)
                FillInner(_tilePositions);
        }

        private void FillTileDataDictionary()
        {
            if (_tileData == null)
                return;

            if (_tileData.Count == 0)
                return;
            
            foreach (var tileData in _tileData)
            {
                foreach (var tile in tileData.Tiles)
                {
                    _dataFromTiles.Add(tile, tileData);
                }
            }
        }

        private Vector2Int[] GetAllTilePositions(Tilemap tilemap)
        {
            var tilePositions = new List<Vector2Int>();
            
            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                var tile = tilemap.GetTile(position);
                
                if (tilemap.HasTile(position) && (!_dataFromTiles.ContainsKey(tile) || !_dataFromTiles[tile].IsIgnoredByOutliner))
                {
                    tilePositions.Add((Vector2Int) position);
                }
            }

            return tilePositions.ToArray();
        }

        private void FillInner(IEnumerable<Vector2Int> positions)
        {
            foreach (Vector3Int position in positions)
            {
                _outline.SetTile(position + _offset, _innerFillTile);
            }
        }

        private void GenerateTiles(TilemapOffsetData[] tilemapOffsetData, IEnumerable<Vector2Int> positions)
        {
            foreach (Vector3Int position in positions)
            {
                foreach (var data in tilemapOffsetData)
                {
                    SetTile(position, data);
                }
            }
        }
        
        private void SetTile(Vector3Int position, TilemapOffsetData data)
        {
            Vector3Int neighborPosition = position + data.Offset;

            _outline.SetTile(neighborPosition + _offset, data.Tile);
        }
        
#endif
    }
}