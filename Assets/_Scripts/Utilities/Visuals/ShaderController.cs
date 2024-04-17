using DG.Tweening;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class ShaderController
    {
        private readonly Renderer _renderer;
        private readonly MaterialPropertyBlock _propertyBlock;

        private readonly int _propertyIndex;
        private readonly int[] _propertyIndexes;

        private Tweener _tweener;

        public ShaderController(Renderer renderer, params string[] properties)
        {
            _renderer = renderer;
            _propertyIndexes = new int[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                _propertyIndexes[i] = Shader.PropertyToID(properties[i]);
            }

            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
        }

        public ShaderController(Renderer renderer, string propertyName)
        {
            _renderer = renderer;
            _propertyIndex = Shader.PropertyToID(propertyName);

            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
        }

        public void SetFloatValue(float value, int propertyId) => InternalSetFloat(value, _propertyIndexes[propertyId]);

        public void SetFloatValue(float value) => InternalSetFloat(value, _propertyIndex);

        public void SetVectorValue(Vector4 value, int propertyId) =>
            InternalSetVector(value, _propertyIndexes[propertyId]);

        public void SetVectorValue(Vector4 value) => InternalSetVector(value, _propertyIndex);

        public void SetColorValue(Color value, int propertyId) => InternalSetColor(value, _propertyIndexes[propertyId]);


        public Tweener Play(float startValue, float endValue, float duration, int propertyId, bool canOverride = true)
        {
            if (canOverride)
                _tweener?.Kill();

            _tweener = DOTween.To(() => startValue, x => InternalSetFloat(x, _propertyIndexes[propertyId]), endValue,
                duration);
            _tweener.SetLink(_renderer.gameObject);

            return _tweener;
        }

        public Tweener Play(float startValue, float endValue, float duration, bool canOverride = true)
        {
#if UNITY_EDITOR
            if (_propertyIndexes != null)
                Debug.LogError("Trying to animate single property when shader controller uses multiple properties");
#endif
            
            if (canOverride)
                _tweener?.Kill();

            _tweener = DOTween.To(() => startValue, x => InternalSetFloat(x, _propertyIndex), endValue, duration);
            _tweener.SetLink(_renderer.gameObject);

            return _tweener;
        }

        private void InternalSetVector(Vector4 value, int propertyIndex)
        {
            _propertyBlock.SetVector(propertyIndex, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private void InternalSetFloat(float value, int propertyIndex)
        {
            _propertyBlock.SetFloat(propertyIndex, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private void InternalSetColor(Color value, int propertyIndex)
        {
            _propertyBlock.SetColor(propertyIndex, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}