using System;
using _Scripts.Managers;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmMoveState : FsmState
    {
        private PlayerController _playerController;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        
        private Vector2 _currentPosition;
        private Vector2Int _playerCellPosition;
        
        public static Action PlayerInTileCenterAction;

        public FsmMoveState(FiniteStateMachine finiteStateMachine, PlayerController playerController) : base(finiteStateMachine)
        {
            _playerController = playerController;
            _rigidbody = playerController.Rigidbody;
            _transform = playerController.transform;
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