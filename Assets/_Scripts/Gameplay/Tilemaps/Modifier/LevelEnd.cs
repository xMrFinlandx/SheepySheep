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
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public void Activate(IPlayerController playerController)
        {
            playerController.SetState<FsmPausedState>();
        }

        private void Start()
        {
            if (!TilemapManager.Instance.TryAddModifiers(transform.position, this))
                Debug.LogError($"Tile is already occupied {transform.gameObject.name}");
        }
    }
}