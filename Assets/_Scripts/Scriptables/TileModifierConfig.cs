using UnityEngine;

namespace _Scripts.Scriptables
{
    public abstract class TileModifierConfig : ScriptableObject
    {
        [SerializeField] private bool _isSingleAtTile;
        [SerializeField] private float _yOffset;

        public bool IsSingleAtTile => _isSingleAtTile;
        public float YOffset => _yOffset;
    }
}