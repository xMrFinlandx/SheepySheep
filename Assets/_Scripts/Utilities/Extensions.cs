using UnityEngine;

namespace _Scripts.Utilities
{
    public static class Extensions
    {
        public static Vector2 CartesianToIsometric(this Vector2 direction)
        {
            return new Vector2(direction.x - direction.y, (direction.x + direction.y) / 2);
        }
    }
}