using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Arrow : MonoBehaviour, IMouseInteraction
    {
        [SerializeField] private bool _isSingleAtTile = false;
        [Space] 
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _counter = 1;

        private Vector2 _direction;
        private ArrowConfig _arrowConfig;
        
        public bool IsSingleAtTile => _isSingleAtTile;

        public void Init(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
            Interact();
            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        public void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_direction);
        }
        
        public Transform GetTransform() => transform;

        public void Interact()
        {
            if (_counter > 3)
                _counter = 0;
            
            var arrowDirectionData = _arrowConfig.ArrowDirectionData[_counter];
            _spriteRenderer.sprite = arrowDirectionData.Sprite;
            _direction = arrowDirectionData.ArrowDirection;

            _counter++;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        private void Restart()
        {
            var key = TilemapManager.Instance.WorldToCell(transform.position);
            TilemapManager.Instance.TryRemoveInteraction(key);
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
    }
}