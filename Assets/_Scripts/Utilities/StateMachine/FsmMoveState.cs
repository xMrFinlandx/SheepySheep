using _Scripts.Managers;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities.StateMachine
{
    public class FsmMoveState : FsmState
    {
        private readonly float _stepSize = .1f;
        private readonly float _speed;
        private readonly string _animationName;
        private readonly Transform _transform;
        
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;
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
            
            _currentPosition = _transform.position;
            _playerCellPosition = TilemapManager.Instance.WorldToCell(_currentPosition);

            if (!TilemapManager.Instance.IsInTilemap(_playerCellPosition))
            {
                FiniteStateMachine.SetState<FsmDiedState>();
                return;
            }
            
            DetectModifiers();

            _previousPosition = _currentPosition;
        }

        private void DetectModifiers()
        {
            Vector3 movement = _currentPosition - _previousPosition;
            var distance = movement.magnitude;
            var steps = Mathf.CeilToInt(distance / _stepSize);
            
            for (int i = 0; i < steps; i++)
            {
                var t = (i + 1) / (float)steps; 
                var stepPosition = Vector3.Lerp(_previousPosition, _currentPosition, t);
                
                if (!TilemapManager.Instance.IsPositionNearTileCenter(stepPosition)) 
                    continue;
                
                PlayerController.PlayerInTileCenterAction?.Invoke();
                ActivateModifiers();
                break;
            }
        }
        
        private void ActivateModifiers() => TilemapManager.Instance.ActivateModifiers(_playerCellPosition, PlayerController);
    }
}