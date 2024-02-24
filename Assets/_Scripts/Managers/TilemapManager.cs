using System.Collections.Generic;
using _Scripts.Player;
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
        private Dictionary<Vector2Int, TileModifiersHandler> _tileModifiersData = new();

        private Tilemap _tilemap;
        private Vector2Int _prevPos;

        [Inject]
        private void Construct(Tilemap tilemap) => _tilemap = tilemap;

        public Vector2Int WorldToCell(Vector2 pos) => (Vector2Int) _tilemap.WorldToCell(pos);

        public bool IsInTilemap(Vector2Int pos) => _tilemap.HasTile((Vector3Int) pos);

        public bool TryAddModifiers(Vector2 pos, ITileModifier modifier)
        {
            var tilemapPos = WorldToCell(pos);
            return TryAddModifiers(tilemapPos, modifier);
        }
        
        public bool TryAddModifiers(Vector2Int key, ITileModifier modifier)
        {
            if (_tileModifiersData.TryGetValue(key, out var value))
            {
                if (value.IsSingleAtTile)
                    return false;
                
                value.Add(modifier);

                return true;
            }

            _tileModifiersData.Add(key, new TileModifiersHandler(modifier));
            return true;
        }

        public void ActivateModifiers(Vector2Int key, IPlayerController playerController)
        {
            if (_prevPos == key)
                return;

            _prevPos = key;
            
            if (_tileModifiersData.TryGetValue(key, out var value))
                value.Activate(playerController);
        }
        
        public void RemoveModifier(Vector2Int key, ITileModifier modifier)
        {
            _tileModifiersData[key].Remove(modifier);
            
            if (_tileModifiersData[key].CanRemoveData)
                _tileModifiersData.Remove(key);
        }
    }
}