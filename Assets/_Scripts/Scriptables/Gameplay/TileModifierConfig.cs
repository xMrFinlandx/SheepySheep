using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    public abstract class TileModifierConfig : ScriptableObject
    {
        [Header("Tile Modifier Settings")]
        [SerializeField] private bool _isSingleAtTile;
        [SerializeField] private float _yOffset;
        [Space]
        [SerializeField] private SoundID _footstepsSound;

        public bool IsSingleAtTile => _isSingleAtTile;
        public float YOffset => _yOffset;
        
        public SoundID FootstepsSound => _footstepsSound;
    }
}