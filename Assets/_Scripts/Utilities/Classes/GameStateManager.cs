using System;
using _Scripts.Utilities.Enums;
using UnityEngine;
using UnityEngine.Rendering;

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

            var isGLES2Available = SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
            var isGLES3Available = SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3;
            Debug.Log($"is gles2 {isGLES2Available}, is gles3 {isGLES3Available}");
        }
    }
}