using _Scripts.Managers;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmMoveState : FsmState
    {
        private readonly float _speed;
        private readonly string _animationName;
        private readonly Transform _transform;
        
        private Vector2 _currentPosition;
        private Vector2Int _playerCellPosition;

        protected Animator Animator { get; }
        protected PlayerController PlayerController { get; }
        protected Rigidbody2D Rigidbody { get; }

        public FsmMoveState(FiniteStateMachine finiteStateMachine, PlayerController playerController, Animator animator, string animationName, float speed) : base(finiteStateMachine)
        {
            PlayerController = playerController;
            Rigidbody = playerController.Rigidbody;
            Animator = animator;

            _animationName = animationName;
            _transform = playerController.transform;
            _speed = speed;
        }

        public override void Enter()
        {
            Animator.speed = 1;
            Animator.enabled = true;
            Animator.Play(_animationName);
        }

        public override void FixedUpdate()
        {
            Rigidbody.velocity = PlayerController.MoveDirection * (_speed * Time.fixedDeltaTime);
        }

        public override void Update()
        {
            _currentPosition = _transform.position;
            _playerCellPosition = TilemapManager.Instance.WorldToCell(_currentPosition);

            if (!TilemapManager.Instance.IsInTilemap(_playerCellPosition))
            {
                FiniteStateMachine.SetState<FsmDiedState>();
                return;
            }

            if (!TilemapManager.Instance.IsPositionNearTileCenter(_currentPosition))
                return;

            PlayerController.PlayerInTileCenterAction?.Invoke();
            ActivateModifiers();
        }
        
        private void ActivateModifiers() => TilemapManager.Instance.ActivateModifiers(_playerCellPosition, PlayerController);
    }
}