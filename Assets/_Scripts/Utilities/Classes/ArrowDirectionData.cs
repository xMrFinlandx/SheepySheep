using System;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    [Serializable]
    public class ArrowDirectionData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private MoveDirectionType _moveDirection;

        public Sprite Sprite => _sprite;
        public Vector2 ArrowDirection => _moveDirection.GetDirectionVector();
    }
}