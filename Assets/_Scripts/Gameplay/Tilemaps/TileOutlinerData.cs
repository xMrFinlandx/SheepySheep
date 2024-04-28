using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Gameplay.Tilemaps
{
    [CreateAssetMenu(fileName = "New Tile Outliner Data", menuName = "Tilemap/Tile Outliner Data", order = 0)]
    public class TileOutlinerData : ScriptableObject
    {
        [SerializeField] private TileBase[] _tiles;
        [SerializeField] private bool _isIgnoredByOutliner = false;

        public IEnumerable<TileBase> Tiles => _tiles;
        public bool IsIgnoredByOutliner => _isIgnoredByOutliner;
    }
}