using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Interactable Config", menuName = "Gameplay/Interactable Config", order = 0)]
    public class InteractableConfig : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private string _gameObjectName;
        [SerializeField] private AnimatorController _animatorController;
        
        public string Name => _gameObjectName;
        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public Sprite IdleSprite => _idleSprite;

        public string AnimationClipName => _animationClip.name;
    }
}