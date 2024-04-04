using _Scripts.Gameplay.Tilemaps.Modifier;
using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Player.Controls;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
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
            var player = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _levelStart.transform.position, Quaternion.identity, null);
            player.Initialize(_levelStart.transform.position, _levelStart.GetDirection());
            
            _inputReader.Init(Camera.main);
            
            Container.Bind<Tilemap>().FromInstance(_tilemap);
            Container.Bind<ArrowConfig>().FromInstance(_arrowConfig);
            Container.Bind<InputReader>().FromInstance(_inputReader);
        }

        public override void Start()
        {
            DataPersistentManager.LoadData();

            TilemapManager.Instance.CollectTileModifiers();
            TilemapAnimatorManager.Instance.Play();
        }
    }
}