using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DynamicArrow : BaseArrow, IMouseInteraction
    {
        private const int _MAX_ROTATIONS_COUNT = 4;
        
        private int _direction = 1;
        private int _rotationsCounter;

        private Vector2Int _key;
        private Vector2 _cartesianDirection;
        private ArrowConfig _arrowConfig;
        private IPlayerController _playerController;

        public override bool IsSingleAtTile => _arrowConfig.IsSingleAtTile;
        public override float YOffset => _arrowConfig.YOffset;
        public override SoundID FootstepsSound => _arrowConfig.FootstepsSound;
        
        private Vector2 OffsetPosition => new(transform.position.x, transform.position.y + YOffset);

        public void Init(ArrowConfig arrowConfig, Vector2Int key)
        {
            _arrowConfig = arrowConfig;
            _key = key;
            
            InitializeShaderController(_arrowConfig);
            SpawnParticleSystem(_arrowConfig.ParticleSystemPrefab);
            Interact();

            ReloadRoomManager.ReloadRoomAction += OnRestart;
            PlayerController.PlayerInNewTileAction += _ => ClearPlayerRotationData();
            PlayerController.PlayerDiedAction += ClearPlayerRotationData;
        }

        private void ClearPlayerRotationData()
        {
            _rotationsCounter = 0;
            _playerController = null;
        }

        public void Interact()
        {
            if (_direction > 3)
                _direction = 0;
            
            var arrowDirectionData = _arrowConfig.ArrowDirectionData[_direction];
            _direction++;
            
            SpriteRenderer.sprite = arrowDirectionData.Sprite;
            _cartesianDirection = arrowDirectionData.ArrowDirection;
            
            PlayShineAnimation();
            ShaderController.SetVectorValue(_cartesianDirection.CartesianToIsometric(), 0);

            if (_rotationsCounter >= _MAX_ROTATIONS_COUNT || _playerController == null ||
                Vector2.Distance(transform.position, _playerController.Transform.position) > 100)
                return;

            _rotationsCounter++;
            Activate(_playerController);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public override void Activate(IPlayerController playerController)
        {
            _playerController = playerController;
            playerController.SetMoveDirectionAndPosition(_cartesianDirection, OffsetPosition);
            
            BroAudio.Play(_arrowConfig.ContactSound);
            
            PlayShineAnimation();
        }
        
        private void OnRestart()
        {
            TilemapManager.Instance.TryRemoveInteraction(_key);
        }

        private void OnValidate() => GetSpriteRenderer();

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= OnRestart;
            PlayerController.PlayerInNewTileAction -= _ => ClearPlayerRotationData();
            PlayerController.PlayerDiedAction -= ClearPlayerRotationData;
        }
    }
}