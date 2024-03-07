using _Scripts.Managers;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class Coin : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = false;
        
        private bool _isEnabled = false;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _isEnabled = true;
            
            print("COIN");
        }
        
        public Transform GetTransform() => transform;
    }
}