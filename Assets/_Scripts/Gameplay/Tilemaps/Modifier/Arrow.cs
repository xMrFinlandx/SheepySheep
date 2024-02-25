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
        private bool _isEnabled = false;

        private Vector2 _direction;
        private ArrowConfig _arrowConfig;
        
        public bool IsSingleAtTile => _isSingleAtTile;

        public void Init(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
            Interact();
        }

        public void Activate(IPlayerController playerController)
        {
            if (_isEnabled)
                return;

            playerController.SetMoveDirection(_direction);

            _isEnabled = true;

            print("ARROW");
        }

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
            print("arrow remove");

            Destroy(gameObject);
        }

        private void OnValidate()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }
    }
}