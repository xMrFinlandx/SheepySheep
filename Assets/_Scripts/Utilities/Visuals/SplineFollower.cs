using PathCreation;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class SplineFollower : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction;
        [SerializeField] private float _speed = 5;
        private float _distanceTravelled;

        private void FixedUpdate()
        {
            _distanceTravelled += _speed * Time.fixedDeltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
        }
    }
}