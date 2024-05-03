using Ami.BroAudio;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
{
    public abstract class InteractableConfig : TileModifierConfig
    {
        [Header("Interaction Animator settings")]
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [Space]
        [SerializeField] private bool _showAnimations;
        [SerializeField] private bool _showShader;
        [Space]
        [ShowIf(nameof(_showAnimations))]
        [SerializeField] private AnimationClip _animationClip;
#if UNITY_EDITOR
        [ShowIf(nameof(_showAnimations))]
        [SerializeField] private AnimatorController _animatorController;
#endif
        [Space]
        [ShowIf(nameof(_showShader))] 
        [SerializeField] private Material _material;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private string _gameObjectName;
        public string Name => _gameObjectName;
        public AnimatorController AnimatorController => _animatorController;
#endif

        public Material Material => _material;
        
        public string AnimationClipName => _animationClip.name;
        
        public Sprite IdleSprite => _idleSprite;
    }
}