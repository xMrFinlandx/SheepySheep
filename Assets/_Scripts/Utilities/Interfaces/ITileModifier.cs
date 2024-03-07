using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface ITileModifier
    {
        public bool IsSingleAtTile { get; }

        public void Activate(IPlayerController playerController);

        public Transform GetTransform();
        
#if UNITY_EDITOR
        public void MoveToCurrentTileCenter(TilemapManager tilemapManager)
        {
            var cellPos = tilemapManager.WorldToCell(GetTransform().position);
            var worldPos = tilemapManager.CellToWorld(cellPos);
            
            GetTransform().position = worldPos;
        }
#endif
    }
}