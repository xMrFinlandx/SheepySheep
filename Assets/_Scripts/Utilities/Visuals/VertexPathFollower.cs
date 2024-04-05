/*using PathCreation;*/

using UnityEngine;
using UnityEngine.Splines;

namespace _Scripts.Utilities.Visuals
{
    public class VertexPathFollower : MonoBehaviour
    {
        [SerializeField] private SplineAnimate _splineAnimate;
        
        
        private float _distanceTravelled;

        public void Set(SplineContainer spline)
        {
            
            _splineAnimate.Container = spline;
        }


        /*private float _speed;
        private VertexPath _path;
        private EndOfPathInstruction _instruction;

        public void Init(VertexPath path, EndOfPathInstruction instruction, float speed)
        {
            _path = path;
            _instruction = instruction;
            _speed = speed;
        }

        private void FixedUpdate()
        {
            _distanceTravelled += _speed * Time.fixedDeltaTime;
            transform.position = _path.GetPointAtDistance(_distanceTravelled, _instruction);
        }*/
    }
}