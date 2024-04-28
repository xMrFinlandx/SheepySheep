using System.Collections.Generic;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Gameplay.Tilemaps
{
    [CreateAssetMenu(fileName = "New Tile Sound Data", menuName = "Tilemap/Tile Sound Data", order = 0)]
    public class TileSoundData : ScriptableObject
    {
        [SerializeField] private TileBase[] _tiles;
        [SerializeField] private AudioClip[] _clips;
        
        public IReadOnlyList<TileBase> Tiles => _tiles;
        
        public AudioClip GetRandomClip() => _clips.GetRandomItem();
    }
}