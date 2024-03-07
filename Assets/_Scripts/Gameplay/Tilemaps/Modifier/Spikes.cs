using System;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Spikes : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [SerializeField] private SpikeConfig _spikeConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _animationName;

        private bool _isTrapEnabled = true;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        public void Activate(IPlayerController playerController)
        {
            if (!_isTrapEnabled)
                return;
            
            playerController.SetState<FsmDiedState>();
        }

        public Transform GetTransform() => transform;
        
        public void Restart()
        {
            _isTrapEnabled = true;
            _animator.enabled = false;
            _spriteRenderer.sprite = _spikeConfig.IdleSprite;
        }

        private void Start()
        {
            _animator.enabled = false;
            Invoke(nameof(Play), 2f);
        }

        private void Play()
        {
            _isTrapEnabled = false;
            _animator.enabled = true;
            _animator.Play(_animationName);
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _spriteRenderer.sprite = _spikeConfig.IdleSprite;
            _animationName = _spikeConfig.AnimationClipName;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _spikeConfig.AnimatorController;
#endif
        }
    }
}