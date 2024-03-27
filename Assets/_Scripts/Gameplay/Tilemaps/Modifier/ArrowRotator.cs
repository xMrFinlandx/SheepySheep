using System;
using _Scripts.Managers;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class ArrowRotator : MonoBehaviour, ITileModifier, IRestartable
    {
        private bool _enabled = true;

        public static Action RotateArrowAction;
        
        public float YOffset => 0f;
        public bool IsSingleAtTile => true;
        
        public Transform GetTransform() => transform;
        
        public void Activate(IPlayerController playerController)
        {
            if (!_enabled)
                return;

            _enabled = false;
            RotateArrowAction?.Invoke();
        }
        
        public void Restart()
        {
            _enabled = true;
        }
        
        private void Start()
        {
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
    }
}