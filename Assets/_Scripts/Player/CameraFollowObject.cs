using _Scripts.Utilities;
using Cinemachine;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.Player
{
    public class CameraFollowObject : Singleton<CameraFollowObject>
    {
        [SerializeField] private bool _isXEnabled = true;
        [SerializeField] private bool _isYEnabled = true;
        
        private Transform _target;

        public void InitTarget(Transform target)
        {
            _target = target;
            
            var vc = FindAnyObjectByType<CinemachineVirtualCamera>();
            vc.Follow = transform;
        }

        private void Update()
        {
            if (!_isYEnabled && !_isXEnabled)
                return;

            var positionX = _isXEnabled ? _target.position.x : transform.position.x;
            var positionY = _isYEnabled ? _target.position.y : transform.position.y;

            transform.position = new Vector3(positionX, positionY);
        }
    }
}