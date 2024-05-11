﻿using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    public abstract class BaseCollectable : MonoBehaviour, IRestartable, ITileModifier
    {
        [SerializeField] private CollectableConfig _collectableConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;
        
        private Vector2 _cashedPosition;
        private Sequence _sequence;
        
        public bool IsSingleAtTile => _collectableConfig.IsSingleAtTile;
        public float YOffset => _collectableConfig.YOffset;
        
        public SoundID FootstepsSound => _collectableConfig.FootstepsSound;

        protected CollectableConfig Config => _collectableConfig;
        
        protected bool IsEnabled { get; set; } = false;
        
        protected string AnimationName => _animationName;
        
        protected Animator Animator => _animator;
        
        public Transform GetTransform() => transform;

        public void CashSpawnPosition()
        {
            _cashedPosition = transform.position;
        }
        
        protected void ResetProgress()
        {
            transform.position = _cashedPosition;
            _spriteRenderer.color = Color.white;
            _spriteRenderer.enabled = true;
            IsEnabled = false;
        }

        protected void PlayCollectedAnimation()
        {
            _sequence = DOTween.Sequence();

            AppendSequence(_collectableConfig.JumpValue, _collectableConfig.JumpDuration, _collectableConfig.JumpEase);
            AppendSequence(_collectableConfig.FallValue, _collectableConfig.FallDuration, _collectableConfig.FallEase);

            _sequence.Join(_spriteRenderer
                .DOFade(0f, _collectableConfig.FallDuration + _collectableConfig.JumpDuration)
                .OnComplete(() => _spriteRenderer.enabled = false));
        }

        protected void InitializeComponents()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _spriteRenderer.sprite = _collectableConfig.IdleSprite;
            _animationName = _collectableConfig.AnimationClipName;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _collectableConfig.AnimatorController;
            name = _collectableConfig.Name;
#endif
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
        
        public abstract void Activate(IPlayerController playerController);

        public abstract void Restart();
    }
}