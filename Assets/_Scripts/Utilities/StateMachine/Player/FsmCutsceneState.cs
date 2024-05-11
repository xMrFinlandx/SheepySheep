using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine.Player
{
    public class FsmCutsceneState : FsmState
    {
        private readonly PlayerController _playerController;
        private readonly Rigidbody2D _rigidbody;
        private readonly Animator _animator;
        private readonly Transform _transform;
        private readonly int _animationHash;
        private readonly float _speed;

        private Vector2Int _currentCellPosition;
        private Vector2Int _previousCellPosition;
        
        public FsmCutsceneState(FiniteStateMachine finiteStateMachine, PlayerController playerController, Animator animator, int animationHash, float speed) : base(finiteStateMachine)
        {
            _playerController = playerController;
            _rigidbody = playerController.Rigidbody;
            _transform = playerController.transform;
            _animator = animator;
            _animationHash = animationHash;
            _speed = speed;
        }

        public override void Enter()
        {
            _animator.speed = 1;
            _animator.enabled = true;
            _animator.Play(_animationHash);
            GameStateManager.SetState(GameStateType.Cutscene);
        }

        public override void FixedUpdate()
        {
            _rigidbody.velocity = _playerController.MoveDirection * (_speed * Time.fixedDeltaTime);
            _currentCellPosition = TilemapManager.Instance.WorldToCell(_transform.position);
            
            if (!TilemapManager.Instance.IsInTilemap(_currentCellPosition) || _previousCellPosition == _currentCellPosition)
                return;

            PlayerController.PlayerInNewTileAction?.Invoke(_currentCellPosition);
            _previousCellPosition = _currentCellPosition;
        }
    }
}