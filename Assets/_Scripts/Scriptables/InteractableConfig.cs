using NaughtyAttributes;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace _Scripts.Scriptables
{
    public abstract class InteractableConfig : TileModifierConfig
    {
        [Header("Interaction Animator settings")]
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private string _gameObjectName;
        [SerializeField] private AnimatorController _animatorController;
        
        public string Name => _gameObjectName;
        public AnimatorController AnimatorController => _animatorController;
#endif

        public string AnimationClipName => _animationClip.name;
        
        public Sprite IdleSprite => _idleSprite;
    }
}