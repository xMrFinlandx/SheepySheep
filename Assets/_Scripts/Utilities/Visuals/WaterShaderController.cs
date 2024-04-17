using _Scripts.Scriptables;
using _Scripts.Utilities.Classes;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class WaterShaderController : MonoBehaviour
    {
        [SerializeField] private ShaderProperty<float> _foamProperty = new("_FoamOpacity", .5f);
        [SerializeField] private WaterOffsetConfig _waterOffsetConfig;
        [Space]
        [SerializeField] private Ease _ease = Ease.InQuart;
        [Space] 
        [SerializeField] private Renderer _renderer;
        
        private ShaderController _shaderController;
        
        public void Play(float duration)
        {
            _shaderController.Play(0, _foamProperty.DefaultValue, duration).SetEase(_ease);
        }
        
        public void Init()
        {
            _shaderController = new ShaderController(_renderer, _foamProperty, _waterOffsetConfig.PropertyName);
            _shaderController.SetVectorValue(_waterOffsetConfig.RandomOffset, 1);
        }
        
        private void OnValidate()
        {
            _renderer = GetComponent<Renderer>();
        }
    }   
}