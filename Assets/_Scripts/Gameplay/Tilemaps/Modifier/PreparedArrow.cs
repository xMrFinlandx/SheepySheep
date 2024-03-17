using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PreparedArrow : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [SerializeField] private MoveDirectionType _direction;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Header("Shader Settings")]
        [SerializeField] private string _vectorProperty;
        [SerializeField] private string _floatProperty;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;
        [SerializeField] private Ease _fadeOutEase;

        private ShaderController _shaderController;
        
        private ArrowConfig _arrowConfig;

        public bool IsSingleAtTile => _isSingleAtTile;

        [Inject]
        private void Construct(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
        }

        public void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_direction.GetDirectionVector());
            PlayShine();
        }
        
        public Transform GetTransform() => transform;

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void PlayShine()
        {
            _shaderController.Play(0, 1, _fadeInDuration, 1)
                .OnComplete(() => _shaderController.Play(1, 0, _fadeOutDuration, 1, _fadeOutEase));
        }

        private void Start()
        {
            var data = _arrowConfig.GetDataByDirection(_direction);
            _spriteRenderer.sprite = data.Sprite;

            _shaderController = new ShaderController(_spriteRenderer, _vectorProperty, _floatProperty);
            _shaderController.SetVectorValue(_direction.GetDirectionVector().CartesianToIsometric(), 0);
        }
    }
}