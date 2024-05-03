using Ami.BroAudio;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    [CreateAssetMenu(fileName = "New Collectable Config", menuName = "Gameplay/Collectable Config", order = 0)]
    public class CollectableConfig : InteractableConfig
    {
        [Header("Sounds")] 
        [SerializeField] private SoundID _collectedSound;
        [Header("Collect Animation Settings")]
        [SerializeField] private float _jumpValue = 1f;
        [SerializeField] private float _fallValue = 2f;
        [SerializeField] private float _jumpDuration = .25f;
        [SerializeField] private float _fallDuration = .5f;

        [SerializeField] private Ease _jumpEase = Ease.OutQuad;
        [SerializeField] private Ease _fallEase = Ease.InQuad;
        
        public float JumpValue => _jumpValue;
        public float FallValue => _fallValue;
        public float JumpDuration => _jumpDuration;
        public float FallDuration => _fallDuration;
        
        public Ease JumpEase => _jumpEase;
        public Ease FallEase => _fallEase;

        public SoundID CollectedSound => _collectedSound;
    }
}