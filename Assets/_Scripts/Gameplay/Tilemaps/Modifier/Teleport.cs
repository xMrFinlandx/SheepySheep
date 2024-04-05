using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

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
        
        private float _controlPointHeight => _teleportConfig.BezierControlPointHeight;
        
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
        }

        public Transform GetTransform() => transform;

        public void Restart()
        {
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
        }

        private void OnTilemapAnimated()
        {
            Vector2 mainTeleportPosition = transform.position;
            Vector2 linkedTeleportPosition = _linkedTeleport.transform.position;
            
            var knots = new[]
            {
                new BezierKnot(new float3(mainTeleportPosition.x, mainTeleportPosition.y, 0), float3.zero, new float3(0, _controlPointHeight, 0)),
                new BezierKnot(new float3(linkedTeleportPosition.x, linkedTeleportPosition.y, 0), new float3(0, _controlPointHeight, 0), float3.zero)
            };
            
            var spline = new Spline(knots);
            var pathFollower = Instantiate(_teleportConfig.PathFollower, mainTeleportPosition, Quaternion.identity);
            var container = Instantiate(_teleportConfig.SplineContainer, Vector3.zero, Quaternion.identity);

            container.Spline = spline;
            
            pathFollower.Set(container);
        }

        private void OnDisable()
        {
            if (!_isMainTeleport)
                return;
            
            TilemapAnimatorManager.AnimationEndedAction -= OnTilemapAnimated;
        }

        private bool IsNotNullAndNotSame() => _linkedTeleport != null && _linkedTeleport != this;

        private bool IsCorrectPair() => _linkedTeleport != null && _linkedTeleport._isMainTeleport != _isMainTeleport;
        
        private void OnDrawGizmos()
        {
            if (!IsNotNullAndNotSame())
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _linkedTeleport.transform.position);
        }
    }
}