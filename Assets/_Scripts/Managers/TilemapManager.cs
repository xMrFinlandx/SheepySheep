﻿using System.Collections.Generic;
using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Managers
{
    public class TilemapManager : Singleton<TilemapManager>
    {
        [SerializeField] private bool _canShowCenters = false;
        [SerializeField] private float _distanceThreshold = .1f;
        
        private readonly Dictionary<Vector2Int, TileModifiersHandler> _tileModifiersData = new();

        private Tilemap _tilemap;
        private Vector2Int _previousPosition;

        public float YCellSize => _tilemap.cellSize.y;

#if UNITY_EDITOR
        [Header("--- Editor Only ---")] 
        [SerializeField] private Tilemap _editorOnlyMainTilemap;
#endif

        [Inject]
        private void Construct(Tilemap tilemap) => _tilemap = tilemap;
        
        public void CollectTileModifiers()
        {
            var modifiers = Extensions.FindObjectsByInterface<ITileModifier>();

            foreach (var modifier in modifiers)
            {
                if (!TryAddModifiers(modifier.AnchorPosition, modifier))
                    Debug.LogError($"Tile is already occupied {modifier.GetTransform().gameObject.name}");
            }
        }

        public Vector2Int WorldToCell(Vector2 pos)
        {
#if UNITY_EDITOR
            _tilemap = _editorOnlyMainTilemap;
#endif
            
            return (Vector2Int) _tilemap.WorldToCell(pos);
        }

        public bool TryGetTileModifier(Vector2Int key, out TileModifiersHandler modifier)
        {
            if (_tileModifiersData.TryGetValue(key, out var value))
            {
                modifier = value;
                return true;
            }

            modifier = null;
            return false;
        }

        public TileBase GetTileAtCellPosition(Vector3Int position) => _tilemap.GetTile(position);
        
        public Vector2 CellToWorld(Vector2Int pos) => _tilemap.CellToWorld((Vector3Int) pos);

        public bool IsInTilemap(Vector2Int pos) => _tilemap.HasTile((Vector3Int) pos);

        public bool TryAddModifiers(Vector2 pos, ITileModifier modifier)
        {
            if (modifier is BaseCollectable interaction)
                interaction.CashSpawnPosition();
                    
            var tilemapPos = WorldToCell(pos);
            return TryAddModifiers(tilemapPos, modifier);
        }

        public bool TryAddModifiers(Vector2Int key, ITileModifier modifier)
        {
            if (TryGetTileModifier(key, out var value))
            {
                if (value.IsSingleAtTile && modifier.IsSingleAtTile)
                    return false;
                
                value.Add(modifier);

                return true;
            }

            _tileModifiersData.Add(key, new TileModifiersHandler(modifier));
            return true;
        }

        public bool TryRemoveInteraction(Vector2Int key)
        {
            if (!TryGetTileModifier(key, out var value))
                return false;
            
            var isRemoved = value.TryRemove();
                
            if (_tileModifiersData[key].CanRemoveData)
                _tileModifiersData.Remove(key);

            return isRemoved;
        }

        public bool TryInteractInteraction(Vector2Int key)
        {
            return TryGetTileModifier(key, out var value) && value.TryInteract();
        } 
        
        public bool CanAddModifier(Vector2Int key)
        {
            if (!_tilemap.HasTile((Vector3Int) key))
                return false;
            
            if (TryGetTileModifier(key, out var value))
                return !value.IsSingleAtTile;

            return true;
        }

        public bool IsPositionNearTileCenter(Vector2 pos)
        {
            var cellPosition = (Vector3Int) WorldToCell(pos); 
            var tileCenter = _tilemap.GetCellCenterWorld(cellPosition); 
            
            var distance = Vector3.Distance(pos, tileCenter);
            return distance <= _distanceThreshold;
        }

        public void ActivateModifiers(Vector2Int key, IPlayerController playerController)
        {
            if (_previousPosition == key)
                return;

            _previousPosition = key;
            
            if (TryGetTileModifier(key, out var value))
                value.Activate(playerController);
        }
        
        public void RemoveModifier(Vector2Int key, ITileModifier modifier)
        {
            _tileModifiersData[key].Remove(modifier);
            
            if (_tileModifiersData[key].CanRemoveData)
                _tileModifiersData.Remove(key);
        }
        
        public void SetTransformToCurrentTileCenter(Transform targetTransform)
        {
            
#if UNITY_EDITOR
            _tilemap = _editorOnlyMainTilemap;
#endif
            
            var cellPosition = (Vector3Int) WorldToCell(targetTransform.position);
            
            if (!_tilemap.HasTile(cellPosition)) 
                return;
            
            var tileCenter = _tilemap.GetCellCenterWorld(cellPosition);
            targetTransform.position = tileCenter;
        }

        private void OnDrawGizmos()
        {
            if (!_canShowCenters || _tilemap == null)
                return;

            var bounds = _tilemap.cellBounds;

            foreach (var pos in bounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

                if (!_tilemap.HasTile(localPlace))
                    continue;

                var center = _tilemap.GetCellCenterWorld(localPlace);
                Gizmos.DrawSphere(center, 0.1f);
            }
        }
    }
}