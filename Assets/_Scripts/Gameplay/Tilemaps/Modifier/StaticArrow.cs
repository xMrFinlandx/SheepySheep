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
    public class StaticArrow : BaseArrow, IRestartable
    {
        [SerializeField] private MoveDirectionType _direction;
        [SerializeField] private ArrowConfig _arrowConfig;
        
        public override bool IsSingleAtTile => _arrowConfig.IsSingleAtTile;
        public override float YOffset => _arrowConfig.YOffset;

        private MoveDirectionType _currentDirection;

        [Inject]
        private void Construct(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
        }

        public override void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_currentDirection.GetDirectionVector());
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
        }

        private void Start()
        {
            _currentDirection = _direction;
            
            InitializeShaderController(_arrowConfig);
            SpawnParticleSystem(_arrowConfig);
            
            ApplyData(_direction);
        }

        private void ApplyData(MoveDirectionType direction)
        {
            var data = _arrowConfig.GetDataByDirection(direction);
            SpriteRenderer.sprite = data.Sprite;
            
            ShaderController.SetVectorValue(direction.GetDirectionVector().CartesianToIsometric(), 0);
        }

        private void OnDestroy()
        {
            ArrowRotator.RotateArrowAction -= Rotate;
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }

        private void Rotate()
        {
            _currentDirection++;

            if ((int) _currentDirection > 3)
                _currentDirection = 0;

            ApplyData(_currentDirection);
            PlayShineAnimation();
        }

        public void Restart()
        {
            _currentDirection = _direction;
            ApplyData(_direction);
        }
    }
}