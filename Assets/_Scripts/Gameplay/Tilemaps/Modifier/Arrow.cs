using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Arrow : MonoBehaviour, IMouseInteraction
    {
        [SerializeField] private bool _isSingleAtTile = false;
        [Space] 
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Header("Shader Settings")]
        [SerializeField] private string _vectorProperty;
        [SerializeField] private string _floatProperty;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;
        [SerializeField] private Ease _fadeOutEase;

        private ShaderController _shaderController;
        
        private int _counter = 1;

        private Vector2 _cartesianDirection;
        private ArrowConfig _arrowConfig;
        
        public bool IsSingleAtTile => _isSingleAtTile;

        public void Init(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
            _shaderController = new ShaderController(_spriteRenderer, _vectorProperty, _floatProperty);
            Interact();
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        public void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_cartesianDirection);

            PlayShine();
        }

        public Transform GetTransform() => transform;

        public void Interact()
        {
            if (_counter > 3)
                _counter = 0;
            
            var arrowDirectionData = _arrowConfig.ArrowDirectionData[_counter];
            
            _spriteRenderer.sprite = arrowDirectionData.Sprite;
            _cartesianDirection = arrowDirectionData.ArrowDirection;
            
            PlayShine();
            
            _shaderController.SetVectorValue(_cartesianDirection.CartesianToIsometric(), 0);
            
            _counter++;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        private void PlayShine()
        {
            _shaderController.Play(0, 1, _fadeInDuration, 1)
                .OnComplete(() => _shaderController.Play(1, 0, _fadeOutDuration, 1, _fadeOutEase));
        }
        
        private void Restart()
        {
            var key = TilemapManager.Instance.WorldToCell(transform.position);
            TilemapManager.Instance.TryRemoveInteraction(key);
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
    }
}