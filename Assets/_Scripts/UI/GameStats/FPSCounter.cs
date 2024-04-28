using UnityEngine;

namespace _Scripts.Scriptables.UI.GameStats
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private int _frameRange = 60;
        
        private int[] _fpsBuffer;
        private int _fpsBufferIndex;
        public int averageFPS { get; private set; }

        private void Update()
        {
            if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
                InitializeBuffer();
            
            UpdateBuffer();
            CalculateFPS();
        }

        private void InitializeBuffer()
        {
            if (_frameRange <= 0)
                _frameRange = 1;

            _fpsBuffer = new int[_frameRange];
            _fpsBufferIndex = 0;
        }

        private void UpdateBuffer()
        {
            _fpsBuffer[_fpsBufferIndex++] = (int) (1f / Time.unscaledDeltaTime);
            
            if (_fpsBufferIndex >= _frameRange)
                _fpsBufferIndex = 0;
        }

        private void CalculateFPS()
        {
            var sum = 0;
            for (var i = 0; i < _frameRange; i++)
            {
                sum += _fpsBuffer[i];
            }

            averageFPS = sum / _frameRange;
        }
    }
}