using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class Plate : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;

        private bool _isEnabled = false;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _isEnabled = true;
            print("PLATE");
        }

        private void Start()
        {
            if (!TilemapManager.Instance.TryAddModifiers(transform.position, this))
                Debug.LogError($"Tile is already occupied. {gameObject.name}");
        }
    }
}