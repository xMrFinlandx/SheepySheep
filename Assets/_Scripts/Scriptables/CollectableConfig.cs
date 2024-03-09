using NaughtyAttributes;
using UnityEditor.Animations;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Collectable Config", menuName = "Gameplay/Collectable Config", order = 0)]
    public class CollectableConfig : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private AnimatorController _animatorController;

        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public Sprite IdleSprite => _idleSprite;
        
        public string AnimationClipName => _animationClip.name;
    }
}