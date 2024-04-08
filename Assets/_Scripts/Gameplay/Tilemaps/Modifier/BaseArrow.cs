using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public abstract class BaseArrow : MonoBehaviour, ITileModifier
    {
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;
        
        [Foldout("Shader Settings")]
        [SerializeField] private string _vectorProperty = "_Direction";
        [Foldout("Shader Settings")]
        [SerializeField] private string _floatProperty = "_ShineCoefficient";
        [Foldout("Shader Settings")]
        [SerializeField] private float _fadeInDuration = .2f;
        [Foldout("Shader Settings")]
        [SerializeField] private float _fadeOutDuration = .4f;
        [Foldout("Shader Settings")]
        [SerializeField] private Ease _fadeOutEase = Ease.OutExpo;

        private const float _START_SHINE_VALUE = 0;
        private const float _END_SHINE_VALUE = 1;

        public abstract bool IsSingleAtTile { get; }
        public abstract float YOffset { get; }
        
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;
        protected ShaderController ShaderController { get; private set; }
        
        public Transform GetTransform() => transform;

        protected void GetSpriteRenderer() => _spriteRenderer ??= GetComponent<SpriteRenderer>();
        
        protected void InitializeShaderController(ArrowConfig arrowConfig)
        {
            ShaderController = new ShaderController(SpriteRenderer, _vectorProperty, _floatProperty,
                arrowConfig.StarsSpeed.PropertyName, arrowConfig.BackgroundColor.PropertyName,
                arrowConfig.FirstColor.PropertyName, arrowConfig.SecondColor.PropertyName);
            
            ShaderController.SetFloatValue(arrowConfig.StarsSpeed.Value, 2);
            ShaderController.SetColorValue(arrowConfig.BackgroundColor.Value, 3);
            ShaderController.SetColorValue(arrowConfig.FirstColor.Value, 4);
            ShaderController.SetColorValue(arrowConfig.SecondColor.Value, 5);
        }

        protected void SpawnParticleSystem(ArrowConfig arrowConfig)
        {
            Instantiate(arrowConfig.ParticleSystemPrefab, transform.position.IncreaseVectorValue(0, .5f),
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