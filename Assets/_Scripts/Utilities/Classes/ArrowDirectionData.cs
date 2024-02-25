using System;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities.Classes
{
    [Serializable]
    public class ArrowDirectionData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ArrowDirectionType _arrowDirection;

        public Sprite Sprite => _sprite;
        public Vector2 ArrowDirection
        {
            get
            {
                return _arrowDirection switch
                {
                    ArrowDirectionType.BottomRight => Vector2.down,
                    ArrowDirectionType.BottomLeft => Vector2.left,
                    ArrowDirectionType.TopLeft => Vector2.up,
                    ArrowDirectionType.TopRight => Vector2.right,
                    _ => Vector2.down
                };
            }
        }
    }
}