using UnityEngine;

namespace _Scripts.Player
{
    public interface IPlayerController
    {
        public void SetMoveDirection(Vector2 direction);
    }
}