using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class FloatPropertyShaderController : PropertyShaderController
    {
        [Space]
        [SerializeField, MinMaxSlider(0, 30)] private Vector2 _minMaxValue;
        
        public void Play() => Play(_minMaxValue.x, _minMaxValue.y, Duration);

        public void Play(float endValue) => Play(_minMaxValue.x, endValue, Duration);
        
        public void Play(float startValue, float endValue, float duration)
        {
            DOTween.To(() => startValue, SetFloatValue, endValue, duration).SetEase(Ease);
        }
        
        public void SetFloatValue(float value)
        {
            PropertyBlock.SetFloat(PropertyName, value);
            Renderer.SetPropertyBlock(PropertyBlock);
        }

        private void Start()
        {
            Init();
            SetFloatValue(_minMaxValue.x);
        }
    }
}