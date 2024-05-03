using _Scripts.Utilities.Classes;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    [CreateAssetMenu(fileName = "New Arrow Rotator Config", menuName = "Gameplay/Arrow Rotator Config", order = 0)]
    public class ArrowRotatorConfig : InteractableConfig
    {
        [Header("Arrow Rotator")]
        [SerializeField] private SoundID _activationSound;
        [SerializeField] private float _idleShineDuration = 1f;
        [SerializeField] private float _activateShineDuration = .1f;
        [SerializeField] private float _deactivateShineDuration = 1f;
        [Space]
        [SerializeField] private string _shineAmount = "_ShineProgressive";
        [SerializeField] private ShaderProperty<float> _progressiveMultiplier = new("_OverrideProgressiveMultiplier", .1f);

        public string ShineAmount => _shineAmount;
        public ShaderProperty<float> ProgressiveMultiplier => _progressiveMultiplier;

        public SoundID ActivationSound => _activationSound;
        
        public float IdleShineDuration => _idleShineDuration;
        public float ActivateShineDuration => _activateShineDuration;
        public float DeactivateShineDuration => _deactivateShineDuration;
    }
}