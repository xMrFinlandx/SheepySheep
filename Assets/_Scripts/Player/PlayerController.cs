using _Scripts.Managers;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private MoveDirectionType _startMoveDirection;
        [Header("Components")]
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;

        private FiniteStateMachine _finiteStateMachine;

        public float Speed => _speed;
        public Vector2 MoveDirection { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;

        public void SetState<T>() where T : FsmState => _finiteStateMachine.SetState<T>();

        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction.CartesianToIsometric();
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

            InitStateMachine();
            
            _finiteStateMachine.SetState<FsmIdleState>();
        }

        private void InitStateMachine()
        {
            _finiteStateMachine = new FiniteStateMachine();
            
            _finiteStateMachine.AddState(new FsmIdleState(_finiteStateMachine));
            _finiteStateMachine.AddState(new FsmMoveState(_finiteStateMachine, this));
            _finiteStateMachine.AddState(new FsmDiedState(_finiteStateMachine));
            _finiteStateMachine.AddState(new FsmPausedState(_finiteStateMachine));
        }

        private void OnArrowInstantiated()
        {
           _finiteStateMachine.SetState<FsmMoveState>();
        }

        private void FixedUpdate() => _finiteStateMachine.FixedUpdate();

        private void Update() => _finiteStateMachine.Update();

        private void OnDestroy()
        {
            TilemapInteractionsManager.ArrowInstantiatedAction -= OnArrowInstantiated;
        }
    }
}
