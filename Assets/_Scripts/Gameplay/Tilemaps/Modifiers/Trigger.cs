using _Scripts.Utilities.Interfaces;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Gameplay.Tilemaps.Modifiers
{
    public class Trigger : MonoBehaviour, ITileModifier
    {
        [SerializeField] private UnityEvent _event;

        private static readonly Vector3[] _gizmosPoints =
        {
            new(0, .5f), new(1, 0), new(0, -.5f), new(-1, 0)
        };
        
        public float YOffset => 0;
        public bool IsSingleAtTile => false;
        public SoundID FootstepsSound => new();
        
        public Transform GetTransform() => transform;
        
        public void Activate(IPlayerController playerController)
        {
            _event.Invoke();
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(position + new Vector3(0, YOffset), .1f);
            Gizmos.DrawLine(_gizmosPoints[0] + position, _gizmosPoints[3] + position);
            
            for (int i = 1; i < _gizmosPoints.Length; i++)
            {
                Gizmos.DrawLine(_gizmosPoints[i - 1] + position, _gizmosPoints[i] + position);
            }
        }
    }
}