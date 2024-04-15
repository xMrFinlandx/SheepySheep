using _Scripts.Managers;
using _Scripts.Utilities.Visuals;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private Tilemap _tilemap;
        
        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemap);
        }

        public override void Start()
        {
            DataPersistentManager.LoadData();
            
            TilemapAnimatorManager.Instance.Play();
        }
    }
}