using System;
using _Scripts.Managers;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;
using YG;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, IPlayerController, IRestartable
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _speedModifier = 1.8f;
        [SerializeField] private MoveDirectionType _startMoveDirection;
        [Header("Components")]
        [SerializeField, HideInInspector] private Rigidbody2D _rigidbody;
        [SerializeField, HideInInspector] private Animator _animator;
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        [SerializeField, HideInInspector] private CameraFollowObject _cameraFollowObject;
        
        private bool _isStarted = false;

        private Wallet _coinsWallet;
        
        private Vector2 _spawnPosition;
        private FiniteStateMachine _finiteStateMachine;
        
        public static Action PlayerInTileCenterAction;
        
        public Vector2 MoveDirection { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;

        public void SetState<T>() where T : FsmState => _finiteStateMachine.SetState<T>();

        public void InitSpawnPosition(Vector2 spawnPoint) => _spawnPosition = spawnPoint;

        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction.CartesianToIsometric();
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);

            if (direction == Vector2.down)
                _spriteRenderer.flipX = false;
            else if (direction == Vector2.left) 
                _spriteRenderer.flipX = true;
        }

        public void AddCoins(int value)
        {
            _coinsWallet.AddToBuffer(value);
        }

        public void AddDiamonds(int value)
        {
            throw new NotImplementedException();
        }

        public void OnLevelCompleted()
        {
            print("Level completed");
            
            _coinsWallet.ApplyBuffer();
            YandexGame.savesData.coins = _coinsWallet.Balance;
            YandexGame.SaveProgress();
        }

        public void Restart()
        {
            print(_coinsWallet.Buffer);
            
            _isStarted = false;
            _animator.enabled = false;
            transform.position = _spawnPosition;
            _finiteStateMachine.SetState<FsmIdleState>();
            SetMoveDirection(_startMoveDirection.GetDirectionVector());
            _coinsWallet.ResetBuffer();
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
            
            SetMoveDirection(_startMoveDirection.GetDirectionVector());
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
            
            _cameraFollowObject = CameraFollowObject.Instance;
            _cameraFollowObject.InitTarget(transform);
            
            InitStateMachine();

            _animator.enabled = false;
            _finiteStateMachine.SetState<FsmIdleState>();

            LoadSavedData();
        }

        private void LoadSavedData()
        {
            YandexGame.LoadProgress();
            _coinsWallet = new Wallet(YandexGame.savesData.coins);
        }

        private void InitStateMachine()
        {
            _finiteStateMachine = new FiniteStateMachine();
            
            _finiteStateMachine.AddState(new FsmIdleState(_finiteStateMachine));
            _finiteStateMachine.AddState(new FsmMoveState(_finiteStateMachine, this, _animator, "PlayerMove", _speed));
            _finiteStateMachine.AddState(new FsmRunState(_finiteStateMachine, this, _animator, "PlayerMove", _speed, _speedModifier));
            _finiteStateMachine.AddState(new FsmDiedState(_finiteStateMachine, _rigidbody));
            _finiteStateMachine.AddState(new FsmPausedState(_finiteStateMachine));
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
    }
}
