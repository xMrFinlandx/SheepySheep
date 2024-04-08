using System;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    [Serializable]
    public class ColorShaderProperty
    {
        [SerializeField] private string _propertyName;
        [SerializeField, ColorUsage(true, true)] private Color[] _values;
        
        public string PropertyName => _propertyName;
        public Color[] Values => _values;

        public ColorShaderProperty()
        {
        }

        public ColorShaderProperty(string propertyName)
        {
            _propertyName = propertyName;
        }

        public ColorShaderProperty(string propertyName, params Color[] values)
        {
            _values = values;
            _propertyName = propertyName;
        }
        
        public static implicit operator string(ColorShaderProperty shaderProperty) => shaderProperty.PropertyName;
    }
}