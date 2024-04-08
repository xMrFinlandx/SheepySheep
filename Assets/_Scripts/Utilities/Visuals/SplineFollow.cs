using System;
using PathCreation;
using UnityEngine;

namespace _Scripts.Utilities.Visuals
{
    public class SplineFollow : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Loop;
        [SerializeField] private ParticleSystem _particleSystem;
        
        private bool _isPlaying = false;
        private float _travelledDistance;
   
        private VertexPath _vertexPath;

        public void Init(VertexPath vertexPath)
        {
            _vertexPath = vertexPath;
        }

        public void InitStartPositionAndSpeed(float speed, bool startAtStart = true)
        {
            _travelledDistance = 0;
            _speed = speed * (startAtStart ? 1 : -1);

            transform.position = _vertexPath.GetPointAtTime(startAtStart ? 0 : 1, _endOfPathInstruction);
        }

        public void Play()
        {
            _isPlaying = true;
            _particleSystem.Play();
        }

        public void Pause()
        {
            _isPlaying = false;
            _particleSystem.Stop();
        }

        public float GetLoopTime()
        {
            return _vertexPath.length / Mathf.Abs(_speed);
        }

        private void OnValidate()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (!_isPlaying)
                return;
            
            _travelledDistance += _speed * Time.deltaTime;
            transform.position = _vertexPath.GetPointAtDistance(_travelledDistance, _endOfPathInstruction) - _vertexPath.Transform.position;
        }
    }
}