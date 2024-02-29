using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface ITileModifier
    {
        public bool IsSingleAtTile { get; }

        public void Activate(IPlayerController playerController);
        
#if UNITY_EDITOR
        public void MoveToCurrentTileCenter(TilemapManager tilemapManager, Transform transform)
        {
            var cellPos = tilemapManager.WorldToCell(transform.position);
            var worldPos = tilemapManager.CellToWorld(cellPos);
            
            transform.position = worldPos;
        }
#endif
    }
}