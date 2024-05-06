using _Scripts.Managers;
using _Scripts.Player.Controls;
using _Scripts.Utilities.Visuals;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private InputReader _inputReader;
        
        public override void InstallBindings()
        {
            _inputReader.Init(Camera.main);
            
            Container.Bind<Tilemap>().FromInstance(_tilemap);
            Container.Bind<InputReader>().FromInstance(_inputReader);
        }

        public override void Start()
        {
            DataPersistentManager.LoadData();
            
            TilemapAnimatorManager.Instance.Play();
        }
    }
}