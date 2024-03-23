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
        [SerializeField] private WayType _thisPassage;
        [Space]
        [SerializeField] private WayType _passageToSpawn;
        [SerializeField] private SceneField _sceneToLoad;

        public WayType ThisPassage => _thisPassage;
        public WayType PassageToSpawn => _passageToSpawn;
        public SceneField SceneToLoad => _sceneToLoad;
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

        public Transform GetTransform() => transform;
    }
}