using _Scripts.Managers;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface ITileModifier
    {
        public Vector2 AnchorPosition => GetTransform().position + new Vector3(0, YOffset);
        
        public float YOffset { get;}

        public bool IsSingleAtTile { get; }

        public SoundID FootstepsSound { get; }

        public void Activate(IPlayerController playerController);

        public Transform GetTransform();

#if UNITY_EDITOR
        public void MoveToCurrentTileCenter(TilemapManager tilemapManager)
        {
            var cellPos = tilemapManager.WorldToCell(AnchorPosition);
            var worldPos = tilemapManager.CellToWorld(cellPos);

            GetTransform().position = worldPos - new Vector2(0, YOffset) + new Vector2(0, tilemapManager.YCellSize / 2);
        }
#endif
    }
}