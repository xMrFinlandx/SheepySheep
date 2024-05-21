using System;
using _Scripts.Managers;
using _Scripts.Scriptables.Gameplay;
using _Scripts.UI;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine.Player;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelEnd : MonoBehaviour, ITileModifier, IDataPersistence
    {
        [SerializeField] private PlateConfig _plateConfig;
        [Space] 
        [SerializeField] private bool _canLoadScene = true;
        [SerializeField] private SceneField _sceneToLoad;
        
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;

        public static Action SceneCompletedAction;
        
        public float YOffset => _plateConfig.YOffset;
        public bool IsSingleAtTile => _plateConfig.IsSingleAtTile;
        
        public SoundID FootstepsSound => _plateConfig.FootstepsSound;
        
        public Transform GetTransform() => transform;
        
        public void SaveData()
        {
            if (_canLoadScene)
                YandexGame.savesData.TrySetNextScene(_sceneToLoad.SceneName);
            
            YandexGame.savesData.MakeScenePassed(SceneManager.GetActiveScene().name);
        }

        public void LoadData()
        {
        }

        public async void Activate(IPlayerController playerController)
        {
            GameStateManager.SetState(GameStateType.Cutscene);
            playerController.SetState<FsmCutsceneState>();
            DataPersistentManager.SaveData();
            LeaderboardManager.Update(YandexGame.savesData.GetCollectedCoinsCount());

            await Awaitable.WaitForSecondsAsync(1f);

            SceneCompletedAction?.Invoke();
            
            if (!_canLoadScene)
                return;
            
            Fader.Instance.Show(() =>
            {
                SceneManager.LoadScene(_sceneToLoad);
                YandexGame.TryFullscreenShow();
            }, () => !YandexGame.TryFullscreenShow());
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            
#if UNITY_EDITOR
            name = $"Level end (to {_sceneToLoad.SceneName})";
#endif
        }
        
        private void Start()
        {
            LeaderboardManager.Init();
        }

        private void OnDestroy()
        {
            LeaderboardManager.Dispose();
        }
    }
}