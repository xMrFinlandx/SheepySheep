using System.Collections.Generic;
using System.Linq;
using _Scripts.Managers;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Utilities.Visuals
{
    public class TilemapAnimatorManager : Singleton<TilemapAnimatorManager>
    {
        [SerializeField] private SpriteRenderer _spriteRendererPrefab;
        [SerializeField] private int _sortingLayerOrder = 100;
        [Header("Animation")]
        [SerializeField] private Ease _ease;
        [SerializeField] private float _moveTime = 2;
        [SerializeField] private float _colorChangingTime = .5f;
        [SerializeField] private float _delay = .1f;
        [Header("Other")] 
        [SerializeField] private float _ySpawnOffset = 10;
        [SerializeField] private float _spriteCenterOffset = .5f;
        [SerializeField] private WaterShaderController _foamController;

        private const int _FOAM_DURATION = 4;
        
        private readonly Color _transparentColor = new(1, 1, 1, 0);
        private List<SpriteRenderer> _spriteRenderers = new();

        private Tilemap _tilemap;
        private Sequence _sequence;
        
        [Inject]
        private void Construct(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }
        
        public void Play()
        {
            GameStateManager.SetState(GameStateType.Cutscene);
            
            GenerateTiles();
            
            _spriteRenderers = JoinInteractions();
            _tilemap.gameObject.SetActive(false);
            
            Animate();
        }

        private List<SpriteRenderer> JoinInteractions()
        {
            var spriteRenderers = TilemapInteractionsManager.Instance.GetInteractionsSpriteRenderers();
            
            foreach (var spriteRenderer in spriteRenderers)
            {
                var spriteRendererTransform = spriteRenderer.transform;
                var spritePosition = spriteRendererTransform.position;

                spriteRendererTransform.position = spritePosition.IncreaseVectorValue(0, _ySpawnOffset);
                spriteRenderer.color = _transparentColor;
            }
            
            return spriteRenderers.Concat(_spriteRenderers).ToList();
        }

        private void OnAnimationEnded()
        {
            _tilemap.gameObject.SetActive(true);
            transform.KillChildren();
            _spriteRenderers.Clear();
            GameStateManager.SetState(GameStateType.Gameplay);
        }

        private void Animate()
        {
            _foamController.Play(_FOAM_DURATION);
            _sequence = DOTween.Sequence();
            
            foreach (var spriteRenderer in _spriteRenderers)
            {
                _sequence.Join(spriteRenderer.transform.DOMoveY(spriteRenderer.transform.position.y - _ySpawnOffset, _moveTime).SetEase(_ease));
                _sequence.Join(spriteRenderer.DOColor(Color.white, _colorChangingTime).SetEase(_ease));
                _sequence.SetDelay(_delay);
            }

            _sequence.SetLink(gameObject);
            _sequence.OnComplete(OnAnimationEnded);
        }

        private void GenerateTiles()
        {
            foreach (var position in _tilemap.cellBounds.allPositionsWithin)
            {
                if (!_tilemap.HasTile(position)) 
                    continue;

                var spriteRenderer = Instantiate(_spriteRendererPrefab, _tilemap.CellToWorld(position), Quaternion.identity, transform);
                var spriteRendererTransform = spriteRenderer.transform;
                var spritePosition = spriteRendererTransform.position;
                
                spriteRenderer.sprite = _tilemap.GetSprite(position);
                spriteRenderer.color = _transparentColor;
                spriteRenderer.sortingOrder = _sortingLayerOrder - (position.x * 2 + position.y * 2);

                spriteRendererTransform.position = spritePosition.IncreaseVectorValue(0, _ySpawnOffset + _spriteCenterOffset);
                
                _spriteRenderers.Add(spriteRenderer);
            }
            
            _spriteRenderers.Shuffle();
        }
    }
}