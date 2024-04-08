using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface IPlayerController
    {
        public void SetState<T>() where T : FsmState;

        public void SetSpeed(float speed);
        
        public void SetMoveDirection(Vector2 cartesianDirection);

        public void AddCoins(int value);

        public void OnLevelCompleted();

        public Transform GetTransform();
    }
}