using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Gameplay.Tilemaps
{
    [CreateAssetMenu(fileName = "New Tile Sound Data", menuName = "Tilemap/Tile Sound Data", order = 0)]
    public class TileSoundData : ScriptableObject
    {
        [SerializeField] private SoundID _soundID = default;
        [SerializeField] private TileBase[] _tiles;
        
        public IEnumerable<TileBase> Tiles => _tiles;

        public SoundID SoundID => _soundID;
    }
}