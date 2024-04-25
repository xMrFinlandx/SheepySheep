using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Player.Controls;
using _Scripts.Scriptables;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities.Visuals;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private InputReader _inputReader;
        [Space]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private LevelStart _levelStart;
        [Space]
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Tilemap _tilemap;

#if UNITY_EDITOR
        public Tilemap Tilemap => _tilemap;
#endif

        [Button]
        private void GetLevelStart()
        {
            _levelStart = FindAnyObjectByType<LevelStart>();
        }

        public override void InstallBindings()
        {
            _inputReader.Init(Camera.main);
            
            Container.Bind<Tilemap>().FromInstance(_tilemap);
            Container.Bind<ArrowConfig>().FromInstance(_arrowConfig);
            Container.Bind<InputReader>().FromInstance(_inputReader);
            
            var player = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _levelStart.transform.position, Quaternion.identity, null);
            player.Initialize(_levelStart.transform.position, _levelStart.GetDirection());
        }

        public override void Start()
        {
            DataPersistentManager.LoadData();

            TilemapManager.Instance.CollectTileModifiers();
            TilemapAnimatorManager.Instance.Play();
        }
    }
}