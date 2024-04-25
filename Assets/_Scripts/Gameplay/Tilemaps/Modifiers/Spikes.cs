using _Scripts.Managers;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.StateMachine;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Spikes : MonoBehaviour, ITileModifier
    {
        [SerializeField] private SpikeConfig _spikeConfig;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        [SerializeField] private int _triggerId;
        
        [SerializeField, HideInInspector]  private SpriteRenderer _spriteRenderer;

        private bool _enabled = true;
        
        public bool IsSingleAtTile => _spikeConfig.IsSingleAtTile;
        
        public float YOffset => _spikeConfig.YOffset;
        
        public void Activate(IPlayerController playerController)
        {
            if (!_enabled)
                return;
            
            playerController.SetState<FsmDiedState>();
        }

        public Transform GetTransform() => transform;

        private void Start()
        {
            _animator.enabled = false;
            TriggersManager.AllTriggersActivatedAction += Play;
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        private void OnDestroy()
        {
            TriggersManager.AllTriggersActivatedAction -= Play;
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
        
        private void Restart()
        {
            _enabled = true;
            _animator.enabled = false;
            _spriteRenderer.sprite = _spikeConfig.IdleSprite;
        }

        private void Play(int index)
        {
            if (index != _triggerId)
                return;
            
            _enabled = false;
            _animator.enabled = true;
            _animator.PlayUnLoopedClip(_animationName);
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _spriteRenderer.sprite = _spikeConfig.IdleSprite;
            _animationName = _spikeConfig.AnimationClipName;
            _triggerId = _spikeConfig.TriggerId;
            
#if UNITY_EDITOR
            _animator.runtimeAnimatorController = _spikeConfig.AnimatorController;
            name = _spikeConfig.Name;
#endif
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}