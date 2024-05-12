using _Scripts.Player;
using _Scripts.Utilities;
using UnityEngine;
using YG;

namespace _Scripts.Managers
{
    public class AdAfterDeathManager : Singleton<AdAfterDeathManager>
    {
        [SerializeField] private int _deathsBeforeAd;
        
        private int _deathsCounter;
        
        private void Start()
        {
            PlayerController.PlayerDiedAction += OnPlayerDied;
        }

        private void OnDisable()
        {
            PlayerController.PlayerDiedAction -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _deathsCounter++;

            TryShowAd();
        }

        public bool TryShowAd()
        {
            if (_deathsCounter <= _deathsBeforeAd)
                return false;

            if (!YandexGame.Instance.CanShowFullscreen())
                return false;
            
            _deathsCounter = 0;
            return YandexGame.TryFullscreenShow();
        }
    }
}