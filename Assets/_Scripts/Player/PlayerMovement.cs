using System;
using _Scripts.Gameplay.Tilemaps;
using _Scripts.Managers;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private MoveDirectionType _startMoveDirection;
        [Header("Components")]
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;

        public static Action PlayerInTileCenterAction;

        private bool _canMove = false;
        
        private Vector2Int _playerCellPosition;
        
        private Vector2 _currentPosition;
        private Vector2 _moveDirection;

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction.CartesianToIsometric();
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
        }
        
        private void OnValidate()
        {
            _collider ??= GetComponent<CircleCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _rigidbody.gravityScale = 0;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        
        private void Start()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction += OnArrowInstantiated;
            
            SetMoveDirection(_startMoveDirection.GetDirectionVector());
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
        }

        private void OnArrowInstantiated()
        {
            _canMove = true;
        }

        private void ActivateModifiers() => TilemapManager.Instance.ActivateModifiers(_playerCellPosition, this);

        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            
            _rigidbody.AddForce(_moveDirection * (_speed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            if (!_canMove)
                return;
            
            _currentPosition = transform.position;
            _playerCellPosition = TilemapManager.Instance.WorldToCell(_currentPosition);

            if (!TilemapManager.Instance.IsInTilemap(_playerCellPosition))
            {
                Debug.Log("Player is not on a tile in the tilemap.");
                return;
            }

            if (!TilemapManager.Instance.IsPositionNearTileCenter(_currentPosition))
                return;

            PlayerInTileCenterAction?.Invoke();
            ActivateModifiers();
        }

        private void OnDestroy()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction -= OnArrowInstantiated;
        }
    }
}
