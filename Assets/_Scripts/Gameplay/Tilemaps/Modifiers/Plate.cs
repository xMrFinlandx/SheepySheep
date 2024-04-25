using System;
using _Scripts.Managers;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class Plate : MonoBehaviour, ITileModifier
    {
        [SerializeField] private PressPlateConfig _plateConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _pressAnimationName;
        [SerializeField] private int _callId;

        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;
        
        public static Action<int> TriggerEnabledAction;
        
        private bool _isEnabled = false;

        public int CallId => _callId;
        public bool IsSingleAtTile => _plateConfig.IsSingleAtTile;

        public float YOffset => _plateConfig.YOffset;
        
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
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}