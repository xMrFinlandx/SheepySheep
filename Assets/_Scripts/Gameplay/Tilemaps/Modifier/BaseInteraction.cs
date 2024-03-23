﻿using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public abstract class BaseInteraction : MonoBehaviour, IRestartable, ITileModifier
    {
        [SerializeField] private InteractableConfig _interactableConfig;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        
        private Vector2 _cashedPosition;
        private Sequence _sequence;
        
        public bool IsSingleAtTile => _interactableConfig.IsSingleAtTile;
        
        public float YOffset => _interactableConfig.YOffset;
        
        protected bool IsEnabled { get; set; } = false;
        
        protected string AnimationName => _animationName;
        
        protected Animator Animator => _animator;

        protected void ResetProgress()
        {
            transform.position = _cashedPosition;
            _spriteRenderer.color = Color.white;
            _spriteRenderer.enabled = true;
            IsEnabled = false;
        }

        public Transform GetTransform() => transform;

        public void CashSpawnPosition()
        {
            _cashedPosition = transform.position;
        }

        protected void PlayCollectedAnimation()
        {
            _sequence = DOTween.Sequence();

            AppendSequence(_interactableConfig.JumpValue, _interactableConfig.JumpDuration, _interactableConfig.JumpEase);
            AppendSequence(_interactableConfig.FallValue, _interactableConfig.FallDuration, _interactableConfig.FallEase);

            _sequence.Join(_spriteRenderer
                .DOFade(0f, _interactableConfig.FallDuration + _interactableConfig.JumpDuration)
                .OnComplete(() => _spriteRenderer.enabled = false));
        }

        private void AppendSequence(float endValue, float duration, Ease ease)
        {
            _sequence.Append(transform.DOLocalMoveY(transform.position.y + endValue, duration).SetEase(ease));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }

        protected void InitializeComponents()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _spriteRenderer.sprite = _interactableConfig.IdleSprite;
            _animationName = _interactableConfig.AnimationClipName;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _interactableConfig.AnimatorController;
           // name = _interactableConfig.Name;
#endif
        }
        
        public abstract void Activate(IPlayerController playerController);

        public abstract void Restart();
    }
}