using System;
using _Scripts.Utilities.Enums;

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

            CurrentGameState = newGameState;
            GameStateChangedAction?.Invoke(CurrentGameState);
        }
    }
}