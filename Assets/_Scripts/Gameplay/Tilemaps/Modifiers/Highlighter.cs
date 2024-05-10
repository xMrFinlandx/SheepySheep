using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Highlighter : MonoBehaviour, ITileModifier
    {
        [SerializeField] private Gradient _evaluateGradient;
        [SerializeField] private float _evaluateTime = 2;
        [SerializeField] private float _fadeTime = .3f;

        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;

        private TweenerCore<Color, Color, ColorOptions> _tweener;
        private Sequence _sequence;
        
        public float YOffset => 0.5f;
        public bool IsSingleAtTile => false;
        public SoundID FootstepsSound => new();
        
        public Transform GetTransform() => transform;

        public void Activate(IPlayerController playerController)
        {
            Hide(_fadeTime);
        }

        public void Show()
        {
            _tweener.Kill();
            _sequence.Kill();

            _tweener = _spriteRenderer.DOFade(1, _fadeTime)
                .OnComplete(() => _sequence = _spriteRenderer.DOGradientColor(_evaluateGradient, _evaluateTime)
                    .SetLoops(-1, LoopType.Yoyo));
        }

        public void Hide(float time = 0)
        {
            _tweener?.Kill();
            _sequence?.Kill();
            
            _spriteRenderer.color = _evaluateGradient.Evaluate(0);
            _tweener = _spriteRenderer.DOFade(0, time);
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}