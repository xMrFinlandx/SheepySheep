using UnityEditor.Animations;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Spike Config", menuName = "Gameplay/Spike Config", order = 0)]
    public class SpikeConfig : ScriptableObject
    {
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private AnimationClip _animationClip;
        
#if UNITY_EDITOR
        [Space] 
        [SerializeField] private AnimatorController _animatorController;

        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public Sprite IdleSprite => _idleSprite;
        public string AnimationClipName => _animationClip.name;
    }
}