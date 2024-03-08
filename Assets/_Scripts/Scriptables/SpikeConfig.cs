using NaughtyAttributes;
using UnityEditor.Animations;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Spike Config", menuName = "Gameplay/Spike Config", order = 0)]
    public class SpikeConfig : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite _idleSprite;
        [SerializeField, Range(0, 10)] private int _triggerId;
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Header("Editor")] 
        [SerializeField] private AnimatorController _animatorController;

        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public Sprite IdleSprite => _idleSprite;
        public string AnimationClipName => _animationClip.name;

        public int TriggerId => _triggerId;
    }
}