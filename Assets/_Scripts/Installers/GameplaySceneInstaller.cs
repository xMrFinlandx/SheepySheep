using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Player.Controls;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Visuals;
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
        [SerializeField] private MoveDirectionType _moveDirection;
        [SerializeField] private Transform _spawnPoint;
        [Space]
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Tilemap _tilemap;

#if UNITY_EDITOR
        public Tilemap Tilemap => _tilemap;
#endif
        
        public override void InstallBindings()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _spawnPoint.position, Quaternion.identity, null);
            player.Initialize(_spawnPoint.position, _moveDirection.GetDirectionVector());
            
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