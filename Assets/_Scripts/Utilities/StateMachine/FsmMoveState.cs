using System;
using _Scripts.Managers;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmMoveState : FsmState
    {
        private const string _STATE_NAME = "PlayerMove";
        
        private readonly PlayerController _playerController;
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;
        
        private Vector2 _currentPosition;
        private Vector2Int _playerCellPosition;
        
        public static Action PlayerInTileCenterAction;

        public FsmMoveState(FiniteStateMachine finiteStateMachine, PlayerController playerController, Animator animator) : base(finiteStateMachine)
        {
            _animator = animator;
            _playerController = playerController;
            _rigidbody = playerController.Rigidbody;
            _transform = playerController.transform;
        }

        public override void Enter()
        {
            _animator.enabled = true;
            _animator.Play(_STATE_NAME);
        }

        public override void FixedUpdate()
        {
            _rigidbody.AddForce(_playerController.MoveDirection * (_playerController.Speed * Time.fixedDeltaTime));
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

            PlayerInTileCenterAction?.Invoke();
            ActivateModifiers();
        }
        
        private void ActivateModifiers() => TilemapManager.Instance.ActivateModifiers(_playerCellPosition, _playerController);
    }
}