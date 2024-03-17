using DG.Tweening;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class WaterShaderController : MonoBehaviour
    {
        [SerializeField] private string _propertyName;
        [SerializeField] private float _endValue = .5f;
        [SerializeField] private Ease _ease = Ease.InQuart;
        [Space] 
        [SerializeField] private Renderer _renderer;
        
        private ShaderController _shaderController;

        public void Play(float duration)
        {
            _shaderController.Play(0, _endValue, duration, _ease);
        }
        
        private void OnValidate()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            _shaderController = new ShaderController(_renderer, _propertyName);
        }
    }   
}