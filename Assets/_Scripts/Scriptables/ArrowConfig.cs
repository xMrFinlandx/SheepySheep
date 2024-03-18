using System.Collections.Generic;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Arrow Config", menuName = "Gameplay/Arrow Config", order = 0)]
    public class ArrowConfig : ScriptableObject
    {
        [SerializeField] private ParticleSystem.MinMaxGradient _minMaxGradient;
        [SerializeField] private ParticleSystem _particleSystemPrefab;
        [Space]
        [SerializeField] private List<ArrowDirectionData> _arrowDirectionData = new();
        [Space(20)]
        [Foldout("Shader Settings")]
        [SerializeField] private ShaderProperty<Color> _firstColor = new(Color.white, "_Color1");
        [Foldout("Shader Settings")]
        [SerializeField] private ShaderProperty<Color> _secondColor = new(Color.white, "_Color2");
        [Foldout("Shader Settings")]
        [SerializeField] private ShaderProperty<Color> _backgroundColor = new(Color.white, "_MainColor");
        [Foldout("Shader Settings")]
        [SerializeField] private ShaderProperty<float> _starsSpeed = new(2, "_ScrollSpeed");

        public IReadOnlyList<ArrowDirectionData> ArrowDirectionData => _arrowDirectionData;
        public ShaderProperty<float> StarsSpeed => _starsSpeed;
        public ShaderProperty<Color> BackgroundColor => _backgroundColor;

        public ShaderProperty<Color> FirstColor => _firstColor;
        public ShaderProperty<Color> SecondColor => _secondColor;

        public ParticleSystem ParticleSystemPrefab
        {
            get
            {
                var main = _particleSystemPrefab.main;
                main.startColor = _minMaxGradient;
                
                return _particleSystemPrefab; 
            }
        }

        public ArrowDirectionData GetDataByDirection(MoveDirectionType directionType)
        {
            return _arrowDirectionData.Find(item => item.MoveDirectionType == directionType);
        }
    }
}