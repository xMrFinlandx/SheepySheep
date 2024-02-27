using UnityEditor.Animations;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Plate Config", menuName = "Gameplay/Plate Config", order = 0)]
    public class PlateConfig : ScriptableObject
    {
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private AnimationClip _pressAnimationClip;
        
#if UNITY_EDITOR
        [Space] 
        [SerializeField] private AnimatorController _animatorController;

        public AnimatorController AnimatorController => _animatorController;
#endif
        
        public Sprite IdleSprite => _idleSprite;
        public AnimationClip PressAnimationClip => _pressAnimationClip;
    }
}