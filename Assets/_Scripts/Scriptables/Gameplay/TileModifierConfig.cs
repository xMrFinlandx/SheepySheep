using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    public abstract class TileModifierConfig : ScriptableObject
    {
        [Header("Tile Modifier Settings")]
        [SerializeField] private bool _isSingleAtTile;
        [SerializeField] private float _yOffset;

        public bool IsSingleAtTile => _isSingleAtTile;
        public float YOffset => _yOffset;
    }
}