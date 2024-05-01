using _Scripts.Utilities.Visuals;
using Ami.BroAudio;
using UnityEngine;

namespace _Scripts.Scriptables.Gameplay
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
        [Header("Sounds")] 
        [SerializeField] private SoundID _entry;
        [SerializeField] private SoundID _loop;
        [SerializeField] private SoundID _exit;

        public float BezierControlPointHeight => _bezierControlPointHeight;
        public float Speed => _speed;
        
        public SplineFollow PathFollower => _pathFollower;
        
        public SplineFollow PlayerPathFollower => _playerPathFollower;

        public SoundID Entry => _entry;
        public SoundID Loop => _loop;
        public SoundID Exit => _exit;
    }
}