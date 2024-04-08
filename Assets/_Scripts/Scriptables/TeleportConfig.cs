using _Scripts.Utilities.Visuals;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Teleport Config", menuName = "Gameplay/Teleport Config", order = 0)]
    public class TeleportConfig : PlateConfig
    {
        [SerializeField] private float _bezierControlPointHeight = 5f;
        [Space]
        [SerializeField] private SplineFollow _pathFollower;
        [SerializeField] private SplineFollow _playerPathFollower;
        [Space]
        [SerializeField] private float _speed = 8f;

        public float BezierControlPointHeight => _bezierControlPointHeight;
        public float Speed => _speed;
        
        public SplineFollow PathFollower => _pathFollower;
        
        public SplineFollow PlayerPathFollower => _playerPathFollower;
    }
}