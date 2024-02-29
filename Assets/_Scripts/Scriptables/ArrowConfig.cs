using System.Collections.Generic;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Arrow Config", menuName = "Gameplay/Arrow Config", order = 0)]
    public class ArrowConfig : ScriptableObject
    {
        [SerializeField] private List<ArrowDirectionData> _arrowDirectionData = new();

        public IReadOnlyList<ArrowDirectionData> ArrowDirectionData => _arrowDirectionData;

        public ArrowDirectionData GetDataByDirection(MoveDirectionType directionType)
        {
            return _arrowDirectionData.Find(item => item.MoveDirectionType == directionType);
        }
    }
}