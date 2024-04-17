﻿#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Player Animation Config", menuName = "Visuals/Player Animation Config", order = 0)]
    public class PlayerAnimationConfig : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private AnimatorController _animatorController;
#endif
        [SerializeField] private AnimationClip _moveAnimation;
        [SerializeField] private AnimationClip _deathAnimation;

#if UNITY_EDITOR
        public AnimatorController AnimatorController => _animatorController;
#endif
        public int MoveAnimation => Animator.StringToHash(_moveAnimation.name); 
        public int DeathAnimation => Animator.StringToHash(_deathAnimation.name);
    }
}