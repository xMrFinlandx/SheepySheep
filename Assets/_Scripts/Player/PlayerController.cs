using System;
using _Scripts.Managers;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;
using YG;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, IPlayerController, IRestartable, IDataPersistence
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _speedModifier = 1.8f;
        [Header("Components")]
        [SerializeField, HideInInspector] private Rigidbody2D _rigidbody;
        [SerializeField, HideInInspector] private Animator _animator;
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        
        private bool _isStarted = false;
        
        private Vector2 _defaultDirection;
        private Vector2 _spawnPosition;
        
        private Wallet _coinsWallet;
        private Sprite _defaultSprite;
        private FiniteStateMachine _finiteStateMachine;
        
        public static Action PlayerInTileCenterAction;
        
        public Vector2 MoveDirection { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;
        
        public void Initialize(Vector2 spawnPosition, Vector2 cartesianDirection)
        {
            _spawnPosition = spawnPosition;
            _defaultDirection = cartesianDirection;
            _defaultSprite = _spriteRenderer.sprite;
        }

        public void SetState<T>() where T : FsmState => _finiteStateMachine.SetState<T>();
        public void SetSpeed(float speed)
        {
        }

        public Transform GetTransform() => transform;
        
        public void SetMoveDirection(Vector2 cartesianDirection)
        {
            var newDirection = cartesianDirection.CartesianToIsometric();

            if (MoveDirection == newDirection)
                return;
            
            MoveDirection = newDirection;
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);

            if (cartesianDirection == Vector2.down)
                _spriteRenderer.flipX = false;
            else if (cartesianDirection == Vector2.left) 
                _spriteRenderer.flipX = true;
        }

        public void AddCoins(int value)
        {
            _coinsWallet.AddToBuffer(value);
        }

        public void OnLevelCompleted()
        {
            print("Level completed");
        }

        public void Restart()
        {
            _isStarted = false;
            _animator.Play("PlayerMove");
            _animator.enabled = false;
            _spriteRenderer.sprite = _defaultSprite;
            transform.position = _spawnPosition;
            SetMoveDirection(_defaultDirection);
            _coinsWallet.ResetBuffer();
            _finiteStateMachine.SetState<FsmIdleState>();
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _animator ??= GetComponent<Animator>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _rigidbody.gravityScale = 0;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        
        private void Start()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction += OnArrowInstantiated;
            ReloadRoomManager.ReloadRoomAction += Restart;
            
            SetMoveDirection(_defaultDirection);
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
            
            InitStateMachine();

            _animator.enabled = false;
            _finiteStateMachine.SetState<FsmIdleState>();
        }
        
        private void InitStateMachine()
        {
            _finiteStateMachine = new FiniteStateMachine();
            
            _finiteStateMachine.AddState(new FsmIdleState(_finiteStateMachine, _rigidbody));
            _finiteStateMachine.AddState(new FsmMoveState(_finiteStateMachine, this, _animator, "PlayerMove", _speed));
            _finiteStateMachine.AddState(new FsmRunState(_finiteStateMachine, this, _animator, "PlayerMove", _speed, _speedModifier));
            _finiteStateMachine.AddState(new FsmDiedState(_finiteStateMachine, _rigidbody));
            _finiteStateMachine.AddState(new FsmPausedState(_finiteStateMachine, _rigidbody));
        }

        private void OnArrowInstantiated()
        {
            if (_isStarted)
                return;
            
            _isStarted = true;
           _finiteStateMachine.SetState<FsmMoveState>();
        }

        private void FixedUpdate() => _finiteStateMachine.FixedUpdate();

        private void Update() => _finiteStateMachine.Update();

        private void OnDestroy()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction -= OnArrowInstantiated;
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        public void SaveData()
        {
            _coinsWallet.ApplyBuffer();
            YandexGame.savesData.coins = _coinsWallet.Balance;
        }

        public void LoadData()
        {
            _coinsWallet = new Wallet(YandexGame.savesData.coins);
        }
    }
}
