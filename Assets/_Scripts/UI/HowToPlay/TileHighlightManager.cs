using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Player;
using _Scripts.Utilities.Interfaces;
using _Scripts.Utilities.Visuals;
using UnityEngine;

namespace _Scripts.UI.HowToPlay
{
    public class TileHighlightManager : MonoBehaviour, IRestartable
    {
        [SerializeField] private Highlighter[] _highlighters;

        public void _EnableHighlighter(int id)
        {
            _highlighters[id].Show();
        }
        
        public void Restart()
        {
            Hide();
            ShowFirst();
        }

        private void Start()
        {
            PlayerController.PlayerDiedAction += Restart;
            TilemapAnimatorManager.AnimationEndedAction += ShowFirst;
            Hide();
        }

        private void ShowFirst()
        {
            _EnableHighlighter(0);
        }

        private void Hide()
        {
            foreach (var highlighter in _highlighters)
            {
                highlighter.Hide();
            }
        }

        private void OnDisable()
        {
            PlayerController.PlayerDiedAction -= Restart;
            TilemapAnimatorManager.AnimationEndedAction -= ShowFirst;
        }
    }
}