using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class Plate : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [Space] 
        [SerializeField] private PlateConfig _plateConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _pressAnimationName;
        
        private bool _isEnabled = false;
        
        public bool IsSingleAtTile => _isSingleAtTile;
        
        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            _pressAnimationName = _plateConfig.PressAnimationClip.name;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _plateConfig.AnimatorController;
#endif
        }

        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            _animator.enabled = true;
            _animator.Play(_pressAnimationName);
            _isEnabled = true;
        }

        private void Start()
        {
            _animator.enabled = false;
            
            if (!TilemapManager.Instance.TryAddModifiers(transform.position, this))
                Debug.LogError($"Tile is already occupied {gameObject.name}");
        }
    }
}