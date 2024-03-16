using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    public class SpeedGem : MonoBehaviour, ITileModifier, IRestartable
    {
        [SerializeField] private InteractableConfig _interactableConfig;
        [SerializeField] private bool _isIncreaseSpeed;
        [SerializeField] private bool _isSingleAtTile = false;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _animationName;

        public bool IsSingleAtTile => _isSingleAtTile;

        private bool _isEnabled = false;

        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _isEnabled = true;
            
            if (_isIncreaseSpeed)
                playerController.SetState<FsmRunState>();
            else
                playerController.SetState<FsmMoveState>();

            _spriteRenderer.enabled = false;
        }

        public Transform GetTransform() => transform;

        public void Restart()
        {
            _isEnabled = false;
            _spriteRenderer.enabled = true;
        }

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

            _spriteRenderer.sprite = _interactableConfig.IdleSprite;
            _animationName = _interactableConfig.AnimationClipName;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _interactableConfig.AnimatorController;
            name = _interactableConfig.Name;
#endif
        }
    }
}