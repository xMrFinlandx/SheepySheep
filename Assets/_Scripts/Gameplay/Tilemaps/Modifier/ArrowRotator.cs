using System;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ArrowRotator : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private PlateConfig _plateConfig;
        
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        
        private bool _enabled = true;

        public static Action RotateArrowAction;
        
        public float YOffset => _plateConfig.YOffset;
        public bool IsSingleAtTile => _plateConfig.IsSingleAtTile;
        
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

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            
#if UNITY_EDITOR
            name = _plateConfig.Name;
#endif
        }

        private void Start()
        {
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}