using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Utilities.Visuals
{
    public class FloatPropertyShaderController : MonoBehaviour
    {
        [SerializeField] private string _propertyName;
        [Space]
        [SerializeField, MinMaxSlider(0, 30)] private Vector2 _minMaxValue;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private Ease _ease;

        private float _counter;
        
        private Renderer _renderer;
        
        private MaterialPropertyBlock _propertyBlock;

        public void Play() => Play(_minMaxValue.x, _minMaxValue.y, _duration);

        public void Play(float endValue) => Play(_minMaxValue.x, endValue, _duration);
        
        public void Play(float startValue, float endValue, float duration)
        {
            DOTween.To(() => startValue, SetFloatValue, endValue, duration).SetEase(_ease);
        }
        
        private void OnValidate()
        {
            _renderer = GetComponent<TilemapRenderer>();
        }

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
            
            SetFloatValue(_minMaxValue.x);
        }
        
        private void SetFloatValue(float value)
        {
            _propertyBlock.SetFloat(_propertyName, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}