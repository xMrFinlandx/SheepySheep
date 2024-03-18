using System;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{

    [Serializable]
    public class ShaderProperty<T> 
    {
        [SerializeField] private T _value;
        [SerializeField] private string _propertyName;

        public T Value => _value;
        public string PropertyName => _propertyName;

        public ShaderProperty()
        {
        }

        public ShaderProperty(T value)
        {
            _value = value;
        }

        public ShaderProperty(T value, string propertyName)
        {
            _value = value;
            _propertyName = propertyName;
        }
    }
}