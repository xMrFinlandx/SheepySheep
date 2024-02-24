using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Gameplay.Tilemaps
{
    [CreateAssetMenu(fileName = "New Tile Data", menuName = "Tilemap/Tile Data", order = 0)]
    public class TileData : ScriptableObject
    {
        [SerializeField] private TileBase[] _tiles;
        [SerializeField] private float _speed = 10;

        public IEnumerable<TileBase> Tiles => _tiles;
        public float Speed => _speed;
    }
}