using System;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using _Scripts.Utilities.StateMachine.Player;
using _Scripts.Utilities.Visuals;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour, IPlayerController, IRestartable
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _speedModifier = 1.8f;
        [Space] 
        [SerializeField] private float _gravityScale = 2;
        [SerializeField] private float _deathAwaitDuration = 1f;
        [SerializeField] private float _fallDeathAwaitDuration = .4f;
        [Space] 
        [SerializeField] private PlayerAnimationConfig _playerAnimationConfig;

        [Header("Components")] 
        [SerializeField, HideInInspector] private ParticleSystem _deathParticleSystem;
        [SerializeField, HideInInspector] private Rigidbody2D _rigidbody;
        [SerializeField, HideInInspector] private Animator _animator;
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        
        private bool _isStarted = false;
        
        private Vector2 _defaultDirection;
        private Vector2 _spawnPosition;
        
        private FiniteStateMachine _finiteStateMachine;

        public static Action<Vector2Int> PlayerInNewTileAction;
        public static Action PlayerInTileCenterAction;
        public static Action PlayerDiedAction;
        
        public Vector2 MoveDirection { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;
        
        public Transform Transform => transform;
        
        public void Initialize(Vector2 spawnPosition, Vector2 cartesianDirection)
        {
            _spawnPosition = spawnPosition;
            _defaultDirection = cartesianDirection;
        }

        public void SetState<T>() where T : FsmState => _finiteStateMachine.SetState<T>();

        public void SetMoveDirectionAndPosition(Vector2 cartesianDirection, Vector2 position)
        {
            var isometricDirection = cartesianDirection.CartesianToIsometric();

            if (MoveDirection == isometricDirection || _finiteStateMachine.IsEqualsCurrentState<FsmFallState>())
                return;
            
            ResetVelocityAndSetPosition(position);
            MoveDirection = isometricDirection;
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
            
            _spriteRenderer.flipX = isometricDirection.x < 0;
        }

        public void Restart()
        {
            _finiteStateMachine.SetState<FsmIdleState>();
            
            _isStarted = false;
            ResetVelocityAndSetPosition(_spawnPosition);
            SetMoveDirectionAndPosition(_defaultDirection, _spawnPosition);
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _animator ??= GetComponent<Animator>();
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _deathParticleSystem ??= GetComponentInChildren<ParticleSystem>();

            _rigidbody.gravityScale = 0;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        
        private void Start()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction += OnArrowInstantiated;
            ReloadRoomManager.ReloadRoomAction += Restart;
            TilemapAnimatorManager.AnimationEndedAction += Show;
            GameStateManager.GameStateChangedAction += OnGameStateChanged;
            
            InitStateMachine();
            _finiteStateMachine.SetState<FsmInvisibleState>();
            
            SetMoveDirectionAndPosition(_defaultDirection, _spawnPosition);
            TilemapManager.Instance.SetTransformToCurrentTileCenter(transform);
            
            _animator.enabled = false;
        }

        private void OnGameStateChanged(GameStateType state)
        {
            if (state == GameStateType.Paused)
            {
                _finiteStateMachine.SetState<FsmPausedState>();
            }
            else if(_finiteStateMachine.IsEqualsCurrentState<FsmPausedState>())
            {
                _finiteStateMachine.SetPreviousState();
            }
        }

        private void Show()
        {
            _finiteStateMachine.SetState<FsmIdleState>();
        }

        private void InitStateMachine()
        {
            _finiteStateMachine = new FiniteStateMachine();
            
            _finiteStateMachine.AddState(new FsmInvisibleState(_finiteStateMachine, _rigidbody, _spriteRenderer));
            _finiteStateMachine.AddState(new FsmIdleState(_finiteStateMachine, _rigidbody, _spriteRenderer, _animator, _playerAnimationConfig.IdleAnimation));
            _finiteStateMachine.AddState(new FsmMoveState(_finiteStateMachine, this, _animator, _playerAnimationConfig.MoveAnimation, _speed));
            _finiteStateMachine.AddState(new FsmRunState(_finiteStateMachine, this, _animator, _playerAnimationConfig.MoveAnimation, _speed, _speedModifier));
            _finiteStateMachine.AddState(new FsmCutsceneState(_finiteStateMachine, this, _animator, _playerAnimationConfig.MoveAnimation, _speed));
            _finiteStateMachine.AddState(new FsmDiedState(_finiteStateMachine, _rigidbody, _spriteRenderer, _deathParticleSystem, _deathAwaitDuration));
            _finiteStateMachine.AddState(new FsmFallState(_finiteStateMachine, _rigidbody, _spriteRenderer, _animator, _playerAnimationConfig.DeathAnimation, _fallDeathAwaitDuration, _gravityScale));
            _finiteStateMachine.AddState(new FsmPausedState(_finiteStateMachine, _rigidbody, _animator));
        }

        private void OnArrowInstantiated()
        {
            if (_isStarted)
                return;
            
            _isStarted = true;
           _finiteStateMachine.SetState<FsmMoveState>();
        }
        
        private void ResetVelocityAndSetPosition(Vector2 position)
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = position;
        }

        private void FixedUpdate() => _finiteStateMachine.FixedUpdate();

        private void Update() => _finiteStateMachine.Update();

        private void OnDestroy()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction -= OnArrowInstantiated;
            ReloadRoomManager.ReloadRoomAction -= Restart;
            TilemapAnimatorManager.AnimationEndedAction -= Show;
            GameStateManager.GameStateChangedAction -= OnGameStateChanged;
        }
    }
}
