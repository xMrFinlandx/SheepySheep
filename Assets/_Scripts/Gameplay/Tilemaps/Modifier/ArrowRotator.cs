using System;
using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ArrowRotator : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private ArrowRotatorConfig _arrowRotatorConfig;
        [Foldout("Shader Settings")]
        [SerializeField] private string _shineFloatProperty = "_ShineProgressive";
        [Foldout("Shader Settings")]
        [SerializeField] private string _overrideFloatProperty = "_OverrideProgressiveMultiplier";
        [Foldout("Shader Settings")] 
        [SerializeField] private float _defaultOverrideProgressiveValue = .1f;
        [Foldout("Shader Settings")] 
        [SerializeField] private float _idleShineDuration = 1f;
        [Foldout("Shader Settings")] 
        [SerializeField] private float _onActivateShineDuration = .1f;
        [Foldout("Shader Settings")] 
        [SerializeField] private float _onDeactivateShineDuration = 1f;
        
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;

        private ShaderController _shaderController;
        private Tweener _tweener;
        
        private bool _enabled = true;

        public static Action RotateArrowAction;
        
        public float YOffset => _arrowRotatorConfig.YOffset;
        public bool IsSingleAtTile => _arrowRotatorConfig.IsSingleAtTile;
        
        public Transform GetTransform() => transform;
        
        public void Activate(IPlayerController playerController)
        {
            if (!_enabled)
                return;

            _tweener.Kill();
            _shaderController.Play(_defaultOverrideProgressiveValue, 1, _onActivateShineDuration, 1);
            _shaderController.Play(0, 1, _onActivateShineDuration, 0, false).OnComplete(() =>
                _shaderController.Play(1, 0, _onDeactivateShineDuration, 0, false));
            
            _enabled = false;
            RotateArrowAction?.Invoke();
        }
        
        public void Restart()
        {
            _enabled = true;
            StartShineAnimation();
        }

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _arrowRotatorConfig.IdleSprite;
            _spriteRenderer.material = _arrowRotatorConfig.Material;
            
#if UNITY_EDITOR
            name = _arrowRotatorConfig.Name;
#endif
        }

        private void Start()
        {
            ReloadRoomManager.ReloadRoomAction += Restart;

            _shaderController = new ShaderController(_spriteRenderer, _shineFloatProperty, _overrideFloatProperty);
            StartShineAnimation();
        }

        private void StartShineAnimation()
        {
            _shaderController.SetFloatValue(_defaultOverrideProgressiveValue, 1);
            _tweener = _shaderController.Play(0, 1, _idleShineDuration, 0).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}