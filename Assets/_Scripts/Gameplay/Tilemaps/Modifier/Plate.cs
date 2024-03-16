using System;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class Plate : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [Space] 
        [SerializeField] private PlateConfig _plateConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _pressAnimationName;
        [SerializeField] private int _callId;

        public static Action<int> TriggerEnabledAction;
        
        private bool _isEnabled = false;

        public int CallId => _callId;
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _animator.enabled = true;
            _animator.PlayUnLoopedClip(_pressAnimationName);
            _isEnabled = true;
            TriggerEnabledAction?.Invoke(_callId);
        }

        public Transform GetTransform() => transform;
        
        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            _pressAnimationName = _plateConfig.AnimationClipName;
            _callId = _plateConfig.CallId;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _plateConfig.AnimatorController;
            name = _plateConfig.Name;
#endif
        }

        private void Start()
        {
            _animator.enabled = false;
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        private void Restart()
        {
            _isEnabled = false;
            _animator.enabled = false;
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
        }
    }
}