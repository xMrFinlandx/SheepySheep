using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Utilities.Interfaces
{
    public interface IPlayerController
    {
        public Transform Transform { get; }
        
        public void SetState<T>() where T : FsmState;
        
        public void SetMoveDirectionAndArrowPosition(Vector2 cartesianDirection, Vector2 position);

        public void AddCoins(int value);
    }
}