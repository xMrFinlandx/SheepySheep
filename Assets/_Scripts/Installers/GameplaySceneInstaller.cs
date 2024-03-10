using _Scripts.Player;
using _Scripts.Scriptables;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _spawnPoint;
        
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Tilemap _tilemap;

#if UNITY_EDITOR
        public Tilemap Tilemap => _tilemap;
#endif
        
        public override void InstallBindings()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _spawnPoint.position, Quaternion.identity, null);
            player.InitSpawnPosition(_spawnPoint.position);
            
            Container.Bind<Tilemap>().FromInstance(_tilemap);
            Container.Bind<ArrowConfig>().FromInstance(_arrowConfig);
        }
    }
}