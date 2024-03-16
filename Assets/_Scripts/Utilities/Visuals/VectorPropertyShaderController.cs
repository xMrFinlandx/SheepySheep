using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class VectorPropertyShaderController : PropertyShaderController
    {
        [SerializeField] private Vector4 _value;

        public void SetVectorValue(Vector2 value)
        {
            print(value);
            
            PropertyBlock.SetVector(PropertyName, value);
            Renderer.SetPropertyBlock(PropertyBlock);
        }
        
        private void Start()
        {
            Init();
        }
    }
}