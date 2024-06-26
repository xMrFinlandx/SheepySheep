﻿using _Scripts.Scriptables.Gameplay;
using _Scripts.Utilities;
using _Scripts.Utilities.Enums;
using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelStart : MonoBehaviour, ITileModifier
    {
        [SerializeField] private PlateConfig _plateConfig;
        [SerializeField] private MoveDirectionType _moveDirection;
        
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        
        public float YOffset => _plateConfig.YOffset;
        public bool IsSingleAtTile => _plateConfig.IsSingleAtTile;
        
        public SoundID FootstepsSound => _plateConfig.FootstepsSound;
        
        public void Activate(IPlayerController playerController)
        {
        }

        public Transform GetTransform() => transform;

        public Vector2 GetDirection() => _moveDirection.GetDirectionVector();
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _plateConfig.IdleSprite;
            
#if UNITY_EDITOR
            name = _plateConfig.Name;
#endif
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0, YOffset), .1f);
        }
    }
}