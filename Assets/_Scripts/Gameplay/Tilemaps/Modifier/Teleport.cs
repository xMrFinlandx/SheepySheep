using System.Collections;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using NaughtyAttributes;
using PathCreation;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Teleport : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private TeleportConfig _teleportConfig;
        [Space] 
        [ValidateInput(nameof(IsCorrectPair), "It is required to have one main teleport in a pair")]
        [SerializeField] private bool _isMainTeleport;
        [ValidateInput(nameof(IsNotNullAndNotSame), "Linked teleport is Null or Same")]
        [SerializeField] private Teleport _linkedTeleport;
        
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;

        private Vector2[] _points;
        private VertexPath _vertexPath;
        private BezierPath _bezierPath;
        private SplineFollow _splineFollow;
        private SplineFollow _playerSplineFollow;

        private bool _isEnabled = false;
        
        private float _controlPointHeight => _teleportConfig.BezierControlPointHeight;
        
        private SplineFollow _pairPlayerSplineFollow
        {
            get => _playerSplineFollow;
            set
            {
                _playerSplineFollow = value;
                _linkedTeleport._playerSplineFollow = value;
            }
        }

        private bool _isPairEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                _linkedTeleport._isEnabled = value;
            }
        }
        
        public float YOffset => _teleportConfig.YOffset;
        public bool IsSingleAtTile => _teleportConfig.IsSingleAtTile;
        public bool IsMainTeleport => _isMainTeleport;
        
        [Button]
        private void SetLink()
        {
            _linkedTeleport.SetPair(this);
        }

        private void SetPair(Teleport teleport)
        {
            _linkedTeleport = teleport;
        }

        public void Activate(IPlayerController playerController)
        {
            if (_isPairEnabled)
                return;

            _isPairEnabled = true;

            playerController.SetState<FsmIdleState>();
            
            var playerTransform = playerController.GetTransform();
            
            playerTransform.DOScaleX(0, .1f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    playerController.GetTransform().position = _linkedTeleport.GetTransform().position;
                    _pairPlayerSplineFollow.InitStartPositionAndSpeed(4, _isMainTeleport);
                    _pairPlayerSplineFollow.Play();

                    StartCoroutine(Wait(_playerSplineFollow.GetLoopTime(), playerController));
                }
            );
        }

        public Transform GetTransform() => transform;

        public void Restart()
        {
            _isPairEnabled = false;
        }

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _spriteRenderer.sprite = _teleportConfig.IdleSprite;

#if UNITY_EDITOR
            name = _isMainTeleport ? _teleportConfig.Name : $"Linked {_teleportConfig.Name}";
#endif
        }

        private void Start()
        {
            if (!_isMainTeleport)
                return;
            
            TilemapAnimatorManager.AnimationEndedAction += OnTilemapAnimated;
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private IEnumerator Wait(float duration, IPlayerController playerController)
        {
            yield return new WaitForSeconds(duration);
            
            _playerSplineFollow.Pause();
            playerController.GetTransform().DOScaleX(1, .1f);
            playerController.SetState<FsmMoveState>();
        }
        
        private void OnTilemapAnimated()
        {
            Vector2 mainTeleportPosition = transform.position;
            Vector2 linkedTeleportPosition = _linkedTeleport.transform.position;

            _points = new[]
            {
                mainTeleportPosition, 
                mainTeleportPosition + new Vector2(0, _controlPointHeight),
                linkedTeleportPosition + new Vector2(0, _controlPointHeight),
                linkedTeleportPosition
            };
            
            _bezierPath = new BezierPath(_points, false);
            _vertexPath = new VertexPath(_bezierPath, transform);

            _splineFollow = Instantiate(_teleportConfig.PathFollower, mainTeleportPosition, Quaternion.identity);
            _pairPlayerSplineFollow = Instantiate(_teleportConfig.PlayerPathFollower, mainTeleportPosition, Quaternion.identity);
            
            _pairPlayerSplineFollow.Init(_vertexPath);
            
            _splineFollow.Init(_vertexPath);
            _splineFollow.InitStartPositionAndSpeed(_teleportConfig.Speed);
            _splineFollow.Play();
        }

        private void OnDisable()
        {
            if (!_isMainTeleport)
                return;
            
            TilemapAnimatorManager.AnimationEndedAction -= OnTilemapAnimated;
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        private bool IsNotNullAndNotSame() => _linkedTeleport != null && _linkedTeleport != this;

        private bool IsCorrectPair() => _linkedTeleport != null && _linkedTeleport._isMainTeleport != _isMainTeleport;
        
        private void OnDrawGizmos()
        {
            if (!IsNotNullAndNotSame())
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _linkedTeleport.transform.position);

            if (_vertexPath == null)
                return;
            
            Gizmos.color = Color.green;

            for (int i = 0; i < _bezierPath.NumPoints; i++)
            {
                Gizmos.DrawSphere(_bezierPath.GetPoint(i), .2f);
            }
        }
    }
}