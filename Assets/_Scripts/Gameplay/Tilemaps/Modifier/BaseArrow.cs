﻿using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public abstract class BaseArrow : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Foldout("Shader Settings")]
        [SerializeField] private string _vectorProperty = "_Direction";
        [Foldout("Shader Settings")]
        [SerializeField] private string _floatProperty = "_ShineCoefficient";
        [Foldout("Shader Settings")]
        [SerializeField] private float _fadeInDuration = .2f;
        [Foldout("Shader Settings")]
        [SerializeField] private float _fadeOutDuration = .4f;
        [Foldout("Shader Settings")]
        [SerializeField] private Ease _fadeOutEase = Ease.OutExpo;

        private const float _START_SHINE_VALUE = 0;
        private const float _END_SHINE_VALUE = 1;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;

        protected ShaderController ShaderController { get; private set; }
        
        public Transform GetTransform() => transform;

        protected void GetSpriteRenderer() => _spriteRenderer ??= GetComponent<SpriteRenderer>();
        
        protected void InitializeShader(ArrowConfig arrowConfig)
        {
            ShaderController = new ShaderController(SpriteRenderer, _vectorProperty, _floatProperty,
                arrowConfig.StarsSpeed.PropertyName, arrowConfig.BackgroundColor.PropertyName,
                arrowConfig.FirstColor.PropertyName, arrowConfig.SecondColor.PropertyName);
            
            ShaderController.SetFloatValue(arrowConfig.StarsSpeed.Value, 2);
            ShaderController.SetColorValue(arrowConfig.BackgroundColor.Value, 3);
            ShaderController.SetColorValue(arrowConfig.FirstColor.Value, 4);
            ShaderController.SetColorValue(arrowConfig.SecondColor.Value, 5);
        }

        protected void PlayShine()
        {
            ShaderController.Play(_START_SHINE_VALUE, _END_SHINE_VALUE, _fadeInDuration, 1).OnComplete(() =>
                ShaderController.Play(_END_SHINE_VALUE, _START_SHINE_VALUE, _fadeOutDuration, 1, _fadeOutEase));
        }

        public abstract void Activate(IPlayerController playerController);
    }
}