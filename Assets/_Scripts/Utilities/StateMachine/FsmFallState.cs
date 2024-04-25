using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Utilities.Visuals;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmFallState : FsmDiedState
    {
        private readonly int _animationHash;
        private readonly float _gravityScale;
        
        private const string _GROUND_LAYER = "Ground";
        private const string _PLAYER_LAYER = "Player";
        
        private readonly Animator _animator;
        
        public FsmFallState(FiniteStateMachine finiteStateMachine, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer,
            Animator animator, int deathAnimation, float awaitDuration, float gravityScale) : base(finiteStateMachine, rigidbody, spriteRenderer, awaitDuration)
        {
            _animator = animator;
            _animationHash = deathAnimation;
            _gravityScale = gravityScale;
        }

        public override async void Enter()
        {
            var cellPosition = TilemapManager.Instance.WorldToCell(Rigidbody.position);
            TilemapAnimatorManager.Instance.EnableTiles(true);
            SpriteRenderer.sortingLayerName = _GROUND_LAYER;
            SpriteRenderer.sortingOrder = TilemapAnimatorManager.Instance.GetSortingOrder((Vector3Int) cellPosition);
            
            Rigidbody.gravityScale = _gravityScale;
            Rigidbody.velocity = Vector2.zero;
            _animator.Play(_animationHash);
            
            await Awaitable.WaitForSecondsAsync(AwaitDuration);
            
            PlayerController.PlayerDiedAction?.Invoke();
        }

        public override void Exit()
        {
            TilemapAnimatorManager.Instance.EnableTiles(false);
            Rigidbody.gravityScale = 0f;
            SpriteRenderer.sortingLayerName = _PLAYER_LAYER;
            SpriteRenderer.sortingOrder = 10;
        }
    }
}