using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelEnd : MonoBehaviour, ITileModifier
    {
        [SerializeField] private PlateConfig _plateConfig;
        [Space]
        [SerializeField] private SceneField _sceneToLoad;
        
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;
        
        public float YOffset => _plateConfig.YOffset;
        
        public bool IsSingleAtTile => _plateConfig.IsSingleAtTile;
        
        public async void Activate(IPlayerController playerController)
        {
            GameStateManager.SetState(GameStateType.Cutscene);
            playerController.SetState<FsmPausedState>();
            playerController.OnLevelCompleted();
            DataPersistentManager.SaveData();

            await Awaitable.WaitForSecondsAsync(3f);

            SceneTransitionsManager.LoadScene(_sceneToLoad);
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            
#if UNITY_EDITOR
            name = $"Level end (to {_sceneToLoad.SceneName})";
        }
#endif

        public Transform GetTransform() => transform;
    }
}