using _Scripts.Player;
using _Scripts.Utilities;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using TMPro;
using UnityEngine;
using YG;

namespace _Scripts.Managers
{
    public class GameStatisticsManager : Singleton<GameStatisticsManager>
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        
        private void Start()
        {
            PlayerController.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            YandexGame.savesData.DeathsCount++;
            YandexGame.SaveProgress();
        }

        private void FixedUpdate()
        {
            if (GameStateManager.CurrentGameState != GameStateType.Gameplay) 
                return;
            
            YandexGame.savesData.CompletionTime += Time.fixedDeltaTime;
            _textMesh.text = $"{YandexGame.savesData.CompletionTime}";
        }

        private void OnDestroy()
        {
            PlayerController.PlayerDiedAction -= OnPlayerDied;
        }
    }
}