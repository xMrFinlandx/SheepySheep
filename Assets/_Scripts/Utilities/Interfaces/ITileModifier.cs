using _Scripts.Player;

namespace _Scripts.Utilities.Interfaces
{
    public interface ITileModifier
    {
        public bool IsSingleAtTile { get; }

        public void Activate(IPlayerController playerController);
    }
}