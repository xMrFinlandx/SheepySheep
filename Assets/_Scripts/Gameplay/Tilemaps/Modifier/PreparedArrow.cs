using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PreparedArrow : MonoBehaviour, ITileModifier
    {
        [SerializeField] private bool _isSingleAtTile = true;
        [SerializeField] private MoveDirectionType _direction;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ArrowConfig _arrowConfig;

        public bool IsSingleAtTile => _isSingleAtTile;

        [Inject]
        private void Construct(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
            print($"Construct {_arrowConfig == null}");
        }

        public void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_direction.GetDirectionVector());
        }
        
        public Transform GetTransform() => transform;

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            var data = _arrowConfig.GetDataByDirection(_direction);
            _spriteRenderer.sprite = data.Sprite;
        }
    }
}