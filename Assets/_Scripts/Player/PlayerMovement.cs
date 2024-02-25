using _Scripts.Managers;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private Vector2 _startMoveDirection = Vector2.right;
        [Header("Components")]
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private Vector2Int _playerCellPosition;

        private Vector2 _moveDirection;
        private int _counter = 0;

        [Button("Change Direction")]
        private void ChangeDirection()
        {
            if (_counter > 3)
                _counter = 0;

            _moveDirection = _counter switch
            {
                0 => Vector2.left.CartesianToIsometric(),
                1 => Vector2.up.CartesianToIsometric(),
                2 => Vector2.right.CartesianToIsometric(),
                3 => Vector2.down.CartesianToIsometric(),
                _ => _moveDirection
            };

            _counter++;
        }
        
        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction.CartesianToIsometric();
        }
        
        private void Start()
        {
            SetMoveDirection(_startMoveDirection);
        }
        
        private void OnValidate()
        {
            _collider ??= GetComponent<CircleCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _rigidbody.gravityScale = 0;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        
        private void FixedUpdate()
        {
            _rigidbody.AddForce(_moveDirection * (_speed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            _playerCellPosition = TilemapManager.Instance.WorldToCell(transform.position);
            
            if (!TilemapManager.Instance.IsInTilemap(_playerCellPosition))
            {
                Debug.Log("Player is not on a tile in the tilemap.");
                return;
            }
   
            TilemapManager.Instance.ActivateModifiers(_playerCellPosition, this);
        }
    }
}
