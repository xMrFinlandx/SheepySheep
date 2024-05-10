using _Scripts.Player;
using _Scripts.Utilities.Visuals;
using UnityEngine;

namespace _Scripts.UI.HowToPlay
{
    public class HowToPlayCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroupController[] _tips;

        public void _ShowTip(int id)
        {
            if (id > 0)
                _tips[id - 1].InstantHide();
            
            _tips[id].Show();
        }

        public void _HideTip(int id)
        {
            _tips[id].Hide();
        }

        private void Start()
        {
            TilemapAnimatorManager.AnimationEndedAction += ShowFirst;
            PlayerController.PlayerDiedAction += ShowFirst;
        }

        private void ShowFirst()
        {
            foreach (var tip in _tips)
            {
                tip.InstantHide();
            }
            
            _ShowTip(0);
        }

        private void OnDisable()
        {
            TilemapAnimatorManager.AnimationEndedAction -= ShowFirst;
            PlayerController.PlayerDiedAction -= ShowFirst;
        }
    }
}