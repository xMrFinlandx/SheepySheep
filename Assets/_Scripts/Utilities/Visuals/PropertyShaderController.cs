using DG.Tweening;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public abstract class PropertyShaderController : MonoBehaviour
    {
        [SerializeField] private string _propertyName;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private Ease _ease;
        [Space]
        [SerializeField] private Renderer _renderer;

        protected string PropertyName => _propertyName;
        protected float Duration => _duration;
        protected Ease Ease => _ease;
        protected Renderer Renderer => _renderer;
        protected MaterialPropertyBlock PropertyBlock { get; private set; }

        public void Init()
        {
            PropertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(PropertyBlock);
        }

        private void OnValidate()
        {
            _renderer = GetComponent<Renderer>();
        }
    }
}