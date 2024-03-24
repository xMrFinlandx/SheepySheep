using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Utilities.Tools
{
    [Serializable]
    public class TilemapOffsetData
    {
        [SerializeField] private OffsetType _offsetType;
        [SerializeField] private TileBase _tile;

        public TileBase Tile => _tile;
        public Vector3Int Offset => _offsetType.GetOffset();
    }
}