using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Interfaces;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DynamicArrow : BaseArrow, IMouseInteraction
    {
        private int _counter = 1;

        private Vector2 _cartesianDirection;
        private ArrowConfig _arrowConfig;

        public void Init(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
            
            InitializeShader(_arrowConfig);
            SpawnParticleSystem(_arrowConfig);
            Interact();

            ReloadRoomManager.ReloadRoomAction += Restart;
        }

        public void Interact()
        {
            if (_counter > 3)
                _counter = 0;
            
            var arrowDirectionData = _arrowConfig.ArrowDirectionData[_counter];
            
            SpriteRenderer.sprite = arrowDirectionData.Sprite;
            _cartesianDirection = arrowDirectionData.ArrowDirection;
            
            PlayShine();
            
            ShaderController.SetVectorValue(_cartesianDirection.CartesianToIsometric(), 0);
            
            _counter++;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public override void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_cartesianDirection);

            PlayShine();
        }
        
        private void Restart()
        {
            var key = TilemapManager.Instance.WorldToCell(transform.position);
            TilemapManager.Instance.TryRemoveInteraction(key);
        }

        private void OnValidate() => GetSpriteRenderer();

        private void OnDestroy()
        {
            ReloadRoomManager.ReloadRoomAction -= Restart;
        }
    }
}