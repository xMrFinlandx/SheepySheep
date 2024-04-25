using System;
using _Scripts.Managers;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ArrowRotator : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private ArrowRotatorConfig _arrowRotatorConfig;
        
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
            _shaderController.Play(_arrowRotatorConfig.ProgressiveMultiplier.Values[0], 1, _arrowRotatorConfig.ActivateShineDuration, 1);
            _shaderController.Play(0, 1, _arrowRotatorConfig.ActivateShineDuration, 0, false).OnComplete(() =>
                _shaderController.Play(1, 0, _arrowRotatorConfig.DeactivateShineDuration, 0, false));
            
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

            _shaderController = new ShaderController(_spriteRenderer, _arrowRotatorConfig.ShineAmount, _arrowRotatorConfig.ProgressiveMultiplier);
            StartShineAnimation();
        }

        private void StartShineAnimation()
        {
            _shaderController.SetFloatValue(_arrowRotatorConfig.ProgressiveMultiplier.Values[0], 1);
            _tweener = _shaderController.Play(0, 1, _arrowRotatorConfig.IdleShineDuration, 0).SetLoops(-1, LoopType.Yoyo);
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