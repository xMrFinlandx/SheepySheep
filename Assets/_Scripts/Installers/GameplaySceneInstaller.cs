using _Scripts.Scriptables;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private ArrowConfig _arrowConfig;
        [SerializeField] private Tilemap _tilemap;

#if UNITY_EDITOR
        public Tilemap Tilemap => _tilemap;
#endif
        
        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemap);
            Container.Bind<ArrowConfig>().FromInstance(_arrowConfig);
        }
    }
}