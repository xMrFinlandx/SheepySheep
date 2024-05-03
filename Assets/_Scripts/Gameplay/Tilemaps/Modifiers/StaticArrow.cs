using _Scripts.Managers;
using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticArrow : BaseArrow, IRestartable
    {
        [SerializeField] private MoveDirectionType _direction;
        [SerializeField] private ArrowConfig _arrowConfig;
        
        public override bool IsSingleAtTile => _arrowConfig.IsSingleAtTile;
        public override float YOffset => _arrowConfig.YOffset;
        
        public override SoundID FootstepsSound => _arrowConfig.FootstepsSound;

        private Vector2 OffsetPosition => new(transform.position.x, transform.position.y + YOffset);

        private MoveDirectionType _currentDirection;

        public override void Activate(IPlayerController playerController)
        {
            BroAudio.Play(_arrowConfig.ContactSound);
            playerController.SetMoveDirectionAndPosition(_currentDirection.GetDirectionVector(), OffsetPosition);
            PlayShineAnimation();
        }

        private void OnValidate()
        {
            GetSpriteRenderer();
            SpriteRenderer.sprite = _arrowConfig.GetDataByDirection(_direction).Sprite;
        }

        private void Awake()
        {
            ArrowRotator.RotateArrowAction += Rotate;
            ReloadRoomManager.ReloadRoomAction += Restart;
            TilemapAnimatorManager.AnimationEndedAction += SpawnParticles;
        }

        private void SpawnParticles()
        {
            SpawnParticleSystem(_arrowConfig.ParticleSystemPrefab);
        }

        private void Start()
        {
            _currentDirection = _direction;
            
            InitializeShaderController(_arrowConfig);
            ApplyDirection(_direction);
        }

        private void ApplyDirection(MoveDirectionType direction)
        {
            var data = _arrowConfig.GetDataByDirection(direction);
            SpriteRenderer.sprite = data.Sprite;
            
            ShaderController.SetVectorValue(direction.GetDirectionVector().CartesianToIsometric(), 0);
        }

        private void OnDestroy()
        {
            ArrowRotator.RotateArrowAction -= Rotate;
            ReloadRoomManager.ReloadRoomAction -= Restart;
            TilemapAnimatorManager.AnimationEndedAction -= SpawnParticles;
        }

        private void Rotate()
        {
            _currentDirection += _arrowConfig.RotationDirection;

            if ((int) _currentDirection > 3 || (int) _currentDirection < 0)
                _currentDirection = 0;

            ApplyDirection(_currentDirection);
            PlayShineAnimation();
        }

        public void Restart()
        {
            _currentDirection = _direction;
            ApplyDirection(_direction);
        }
    }
}
