﻿using _Scripts.Scriptables;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Tilemaps.Modifier
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticArrow : BaseArrow
    {
        [SerializeField] private MoveDirectionType _direction;
        [SerializeField] private ArrowConfig _arrowConfig;
        
        public override bool IsSingleAtTile => _arrowConfig.IsSingleAtTile;
        public override float YOffset => _arrowConfig.YOffset;

        [Inject]
        private void Construct(ArrowConfig arrowConfig)
        {
            _arrowConfig = arrowConfig;
        }

        public override void Activate(IPlayerController playerController)
        {
            playerController.SetMoveDirection(_direction.GetDirectionVector());
            PlayShineAnimation();
        }
        
        private void OnValidate() => GetSpriteRenderer();
        
        private void Start()
        {
            var data = _arrowConfig.GetDataByDirection(_direction);
            SpriteRenderer.sprite = data.Sprite;

            InitializeShader(_arrowConfig);
            SpawnParticleSystem(_arrowConfig);
            
            ShaderController.SetVectorValue(_direction.GetDirectionVector().CartesianToIsometric(), 0);
        }
    }
}