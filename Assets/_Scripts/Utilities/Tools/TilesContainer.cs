using UnityEngine;

namespace _Scripts.Utilities.Tools
{
    [CreateAssetMenu(fileName = "New Tiles Container", menuName = "Tools/Tiles Container", order = 0)]
    public class TilesContainer : ScriptableObject
    {
        [SerializeField] private TilemapOffsetData[] _tilemapOffsetData;

        public TilemapOffsetData[] TilemapOffsetData => _tilemapOffsetData;
    }
}