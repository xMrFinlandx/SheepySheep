using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Utilities.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public static int GetRandomId<T>(this IEnumerable<T> enumerable) => Random.Range(0, enumerable.Count());

        public static T GetRandomItem<T>(this IList<T> list) => list[list.GetRandomId()];

        public static Vector2 CartesianToIsometric(this Vector2 direction)
        {
            return new Vector2(direction.x - direction.y, (direction.x + direction.y) / 2);
        }
        
        public static IEnumerable<T> FindObjectsByInterface<T>() where T : class
        {
            return Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>();
        }

        public static IList<T> Shuffle<T>(this IList<T> list) where T : class
        {
            var currentIndex = list.Count;

            while (currentIndex > 1)
            {
                currentIndex--;
                var randomIndex = Random.Range(0, currentIndex);
                (list[randomIndex], list[currentIndex]) = (list[currentIndex], list[randomIndex]);
            }

            return list;
        }
        
        public static void KillChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void PlayUnLoopedClip(this Animator animator, string animationName)
        {
            animator.Play(animationName, -1, 0);
        }

        public static Vector2 IncreaseVectorValue(this Vector2 vector, float x, float y)
        {
            return new Vector2(vector.x + x, vector.y + y);
        }

        public static Vector3 IncreaseVectorValue(this Vector3 vector, float x, float y, float z = 0)
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }
    }
}