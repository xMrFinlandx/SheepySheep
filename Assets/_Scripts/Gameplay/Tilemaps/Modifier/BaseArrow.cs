using _Scripts.Scriptables;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public abstract class BaseArrow : MonoBehaviour, ITileModifier
    {
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;

        private const float _START_SHINE_VALUE = 0;
        private const float _END_SHINE_VALUE = 1;

        private float _fadeInDuration;
        private float _fadeOutDuration;
        private Ease _fadeOutEase;

        public abstract bool IsSingleAtTile { get; }
        public abstract float YOffset { get; }
        
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;
        protected ShaderController ShaderController { get; private set; }
        
        public Transform GetTransform() => transform;

        protected void GetSpriteRenderer() => _spriteRenderer ??= GetComponent<SpriteRenderer>();
        
        protected void InitializeShaderController(ArrowConfig arrowConfig)
        {
            _fadeInDuration = arrowConfig.ShineFadeInDuration;
            _fadeOutDuration = arrowConfig.ShineFadeOutDuration;
            _fadeOutEase = arrowConfig.Ease;
            
            ShaderController = new ShaderController(SpriteRenderer, 
                arrowConfig.VectorProperty, arrowConfig.ShineCoefficientProperty,
                arrowConfig.StarsSpeed, arrowConfig.BackgroundColor,
                arrowConfig.FirstColor, arrowConfig.SecondColor);
            
            ShaderController.SetFloatValue(arrowConfig.StarsSpeed.Values[0], 2);
            ShaderController.SetColorValue(arrowConfig.BackgroundColor.Values[0], 3);
            ShaderController.SetColorValue(arrowConfig.FirstColor.Values[0], 4);
            ShaderController.SetColorValue(arrowConfig.SecondColor.Values[0], 5);
        }

        protected void SpawnParticleSystem(ParticleSystem particleSystemPrefab)
        {
            Instantiate(particleSystemPrefab, transform.position.IncreaseVectorValue(0, .5f),
                Quaternion.Euler(-90, 0, 0), transform);
        }

        protected void PlayShineAnimation()
        {
            ShaderController.Play(_START_SHINE_VALUE, _END_SHINE_VALUE, _fadeInDuration, 1).OnComplete(() =>
                ShaderController.Play(_END_SHINE_VALUE, _START_SHINE_VALUE, _fadeOutDuration, 1).SetEase(_fadeOutEase));
        }

        public abstract void Activate(IPlayerController playerController);
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}