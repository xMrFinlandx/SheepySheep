using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Coin : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private CollectableConfig _collectableConfig;
        [SerializeField] private bool _isSingleAtTile = false;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _animationName;
        
        private bool _isEnabled = false;
        
        public bool IsSingleAtTile => _isSingleAtTile;

        public void Restart()
        {
            _spriteRenderer.enabled = true;
            _isEnabled = false;
        }

        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _isEnabled = true;

            _spriteRenderer.enabled = false;
        }
        
        public Transform GetTransform() => transform;
        
        private void Start()
        {
            _animator.Play(_animationName);
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _spriteRenderer.sprite = _collectableConfig.IdleSprite;
            _animationName = _collectableConfig.AnimationClipName;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _collectableConfig.AnimatorController;
#endif
        }
    }
}