using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

namespace _Scripts.Gameplay.Tilemaps
{
    [CreateAssetMenu(fileName = "New Tile Sound Data", menuName = "Tilemap/Tile Sound Data", order = 0)]
    public class TileSoundData : ScriptableObject
    {
        [SerializeField] private SoundID _soundID = default;
        [SerializeField] private TileBase[] _tiles;
        [SerializeField] private AudioResource _audioResource;
        
        public IEnumerable<TileBase> Tiles => _tiles;

        public AudioResource AudioResource => _audioResource;

        public SoundID SoundID => _soundID;
    }
}