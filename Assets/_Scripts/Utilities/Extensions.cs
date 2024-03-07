﻿using System.Collections.Generic;
using System.Linq;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Utilities
{
    public static class Extensions
    {
        public static Vector2 GetDirectionVector(this MoveDirectionType moveDirectionType)
        {
            return moveDirectionType switch
            {
                MoveDirectionType.BottomRight => Vector2.down,
                MoveDirectionType.BottomLeft => Vector2.left,
                MoveDirectionType.TopLeft => Vector2.up,
                MoveDirectionType.TopRight => Vector2.right,
                _ => Vector2.down
            };
        }

        public static Vector2 CartesianToIsometric(this Vector2 direction)
        {
            return new Vector2(direction.x - direction.y, (direction.x + direction.y) / 2);
        }
        
        public static List<T> FindObjectsByInterface<T>() where T : class
        {
            return Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>().ToList();
        }
    }
}