using System;
using _Scripts.Managers;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class LevelEnd : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [Space]
        [SerializeField] private SceneField _sceneToLoad;
        
        public float YOffset => 0;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public async void Activate(IPlayerController playerController)
        {
            GameStateManager.SetState(GameStateType.Cutscene);
            playerController.SetState<FsmPausedState>();
            playerController.OnLevelCompleted();
            DataPersistentManager.SaveData();

            await Awaitable.WaitForSecondsAsync(3f);

            SceneTransitionsManager.Instance.LoadScene(_sceneToLoad);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            name = $"Level end (to {_sceneToLoad.SceneName})";
        }
#endif

        public Transform GetTransform() => transform;
    }
}