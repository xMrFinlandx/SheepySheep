using _Scripts.Utilities.Visuals;
using UnityEngine;
using UnityEngine.Splines;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Teleport Config", menuName = "Gameplay/Teleport Config", order = 0)]
    public class TeleportConfig : PlateConfig
    {
        [SerializeField] private float _bezierControlPointHeight = 5f;
        [Space]
        [SerializeField] private VertexPathFollower _pathFollower;

        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private float _speed = 5f;


        public float BezierControlPointHeight => _bezierControlPointHeight;
        public float Speed => _speed;

        public SplineContainer SplineContainer => _splineContainer;
        
        public VertexPathFollower PathFollower => _pathFollower;
        /*public EndOfPathInstruction EndOfPathInstruction => _endOfPathInstruction;*/
    }
}