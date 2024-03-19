using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Interactable Config", menuName = "Gameplay/Interactable Config", order = 0)]
    public class InteractableConfig : TileModifierConfig
    {
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [Foldout("Animation")]
        [SerializeField] private float _jumpValue = 1f;
        [Foldout("Animation")]
        [SerializeField] private float _fallValue = 2f;
        [Foldout("Animation")]
        [SerializeField] private float _jumpDuration = .25f;
        [Foldout("Animation")]
        [SerializeField] private float _fallDuration = .5f;
        [Foldout("Animation")]
        [SerializeField] private Ease _jumpEase = Ease.OutQuad;
        [Foldout("Animation")]
        [SerializeField] private Ease _fallEase = Ease.InQuad;
        
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private string _gameObjectName;
        [SerializeField] private AnimatorController _animatorController;
        
        public string Name => _gameObjectName;
        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public float JumpValue => _jumpValue;
        public float FallValue => _fallValue;
        public float JumpDuration => _jumpDuration;
        public float FallDuration => _fallDuration;
        
        public Ease JumpEase => _jumpEase;
        public Ease FallEase => _fallEase;
        
        public Sprite IdleSprite => _idleSprite;

        public string AnimationClipName => _animationClip.name;
    }
}