using System;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    public static class GameStateManager
    {
        public static GameStateType CurrentGameState { get; private set; }

        public static Action<GameStateType> GameStateChangedAction;

        public static void SetState(GameStateType newGameState)
        {
            if (newGameState == CurrentGameState)
                return;

            Debug.Log(CurrentGameState);
            
            CurrentGameState = newGameState;
            GameStateChangedAction?.Invoke(CurrentGameState);
        }
    }
}