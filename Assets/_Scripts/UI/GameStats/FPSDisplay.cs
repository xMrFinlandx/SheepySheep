using TMPro;
using UnityEngine;

namespace _Scripts.Scriptables.UI.GameStats
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private FPSCounter _fpsCounter;

        private void Awake()
        {
            _fpsCounter = GetComponent<FPSCounter>();
        }

        private void Update()
        {
            _text.text = "fps " + _fpsCounter.averageFPS;
        }
    }
}