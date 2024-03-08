using NaughtyAttributes;
using UnityEditor.Animations;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Plate Config", menuName = "Gameplay/Plate Config", order = 0)]
    public class PlateConfig : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [SerializeField, Range(0, 10)] private int _callId;
        [SerializeField] private AnimationClip _pressAnimationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private AnimatorController _animatorController;

        public AnimatorController AnimatorController => _animatorController;
#endif

        public int CallId => _callId;
        public Sprite IdleSprite => _idleSprite;
        public string AnimationClipName => _pressAnimationClip.name;
    }
}