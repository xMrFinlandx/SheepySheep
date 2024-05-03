using System.Collections.Generic;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using Ami.BroAudio;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    [CreateAssetMenu(fileName = "New Arrow Config", menuName = "Gameplay/Arrow Config", order = 0)]
    public class ArrowConfig : TileModifierConfig
    {
        [SerializeField] private SoundID _contactSound;
        [SerializeField] private SoundID _rotationSound;
        
        [SerializeField] private bool _isDirectionReversed = false;
        [Space]
        [SerializeField] private ParticleSystem.MinMaxGradient _minMaxGradient;
        [SerializeField] private ParticleSystem _particleSystemPrefab;
        [Space]
        [SerializeField] private List<ArrowDirectionData> _arrowDirectionData = new();
        [Space(20)]
        [Header("Animations")]
        [SerializeField] private float _shineFadeInDuration = .2f;
        [SerializeField] private float _shineFadeOutDuration = .4f;
        [SerializeField] private Ease _fadeOutEase = Ease.OutExpo;
        [Header("Shader properties")]
        [SerializeField] private string _vectorProperty = "_Direction";
        [SerializeField] private string _shineCoefficientProperty = "_ShineCoefficient";
        [Space] 
        [SerializeField] private ShaderProperty<float> _starsSpeed = new("_ScrollSpeed", 2);
        [SerializeField] private ShaderProperty<Color> _firstColor = new("_Color1");
        [SerializeField] private ShaderProperty<Color> _secondColor = new("_Color2");
        [SerializeField] private ShaderProperty<Color> _backgroundColor = new("_MainColor");

        public float ShineFadeInDuration => _shineFadeInDuration;
        public float ShineFadeOutDuration => _shineFadeOutDuration;
        
        public string VectorProperty => _vectorProperty;
        public string ShineCoefficientProperty => _shineCoefficientProperty;

        public Ease Ease => _fadeOutEase;
        
        public SoundID RotationSound => _rotationSound;
        public SoundID ContactSound => _contactSound;
        
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

        public int RotationDirection => _isDirectionReversed ? -1 : 1;

        public ArrowDirectionData GetDataByDirection(MoveDirectionType directionType)
        {
            return _arrowDirectionData.Find(item => item.MoveDirectionType == directionType);
        }
    }
}