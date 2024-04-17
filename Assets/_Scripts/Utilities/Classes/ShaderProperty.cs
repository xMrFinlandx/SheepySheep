using System;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{

    [Serializable]
    public class ShaderProperty<T> 
    {
        [SerializeField] private string _propertyName;
        [SerializeField] private T[] _values;
        
        public string PropertyName => _propertyName;
        
        public T[] Values => _values;
        public T DefaultValue => _values[0];

        public ShaderProperty()
        {
        }

        public ShaderProperty(string propertyName)
        {
            _propertyName = propertyName;
        }

        public ShaderProperty(string propertyName, params T[] values)
        {
            _values = values;
            _propertyName = propertyName;
        }

        public static implicit operator string(ShaderProperty<T> shaderProperty) => shaderProperty.PropertyName;
    }
}