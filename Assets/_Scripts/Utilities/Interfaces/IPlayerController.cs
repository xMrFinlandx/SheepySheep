using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface IPlayerController
    {
        public void SetState<T>() where T : FsmState;
        
        public void SetMoveDirection(Vector2 direction);

        public void AddCoins(int value);

        public void AddDiamonds(int value);

        public void OnLevelCompleted();
    }
}