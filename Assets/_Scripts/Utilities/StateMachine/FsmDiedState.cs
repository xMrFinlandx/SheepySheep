using System;
using _Scripts.Managers;
using _Scripts.Utilities.Visuals;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmDiedState : FsmState
    {
        private readonly int _animationHash;
        private readonly float _fallDuration;
        private readonly float _gravityScale;
        private const string _GROUND_LAYER = "Ground";
        private const string _PLAYER_LAYER = "Player";

        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;
        
        public static Action PlayerDiedAction;
        
        public FsmDiedState([CanBeNull] FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer,
            Animator animator, int deathAnimation, float fallDuration, float gravityScale) : base(finiteStateMachine)
        {
            _spriteRenderer = spriteRenderer;
            _rigidbody = rigidbody;
            _animator = animator;
            _animationHash = deathAnimation;
            _fallDuration = fallDuration;
            _gravityScale = gravityScale;
        }

        public override async void Enter()
        {
            var cellPosition = TilemapManager.Instance.WorldToCell(_rigidbody.position);
            TilemapAnimatorManager.Instance.EnableTiles(true);
            _spriteRenderer.sortingLayerName = _GROUND_LAYER;
            _spriteRenderer.sortingOrder = TilemapAnimatorManager.Instance.GetSortingOrder((Vector3Int) cellPosition);
            
            _rigidbody.gravityScale = _gravityScale;
            _rigidbody.velocity = Vector2.zero;
            _animator.Play(_animationHash);
            
            await Awaitable.WaitForSecondsAsync(_fallDuration);
            
            PlayerDiedAction?.Invoke();
        }

        public override void Exit()
        {
            TilemapAnimatorManager.Instance.EnableTiles(false);
            _rigidbody.gravityScale = 0f;
            _spriteRenderer.sortingLayerName = _PLAYER_LAYER;
            _spriteRenderer.sortingOrder = 10;
        }
    }
}