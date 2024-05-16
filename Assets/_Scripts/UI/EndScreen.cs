using _Scripts.Gameplay.Tilemaps.Modifiers;
using _Scripts.Managers;
using _Scripts.Player.Controls;
using _Scripts.Utilities.Classes;
using _Scripts.Utilities.Enums;
using Ami.BroAudio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Zenject;

namespace _Scripts.UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private SceneField _menuScene;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _resultTextMesh;
        [Space]
        [SerializeField] private CanvasGroupController _canvasGroupController;

        private InputReader _inputReader;

        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void Show()
        {
            GameStateManager.SetState(GameStateType.Paused);
            _inputReader.Disable();
            
            var value = YandexGame.savesData.CompletionTime;
            var deaths = YandexGame.savesData.DeathsCount;
            var coins = YandexGame.savesData.GetCollectedCoinsCount();
            
            var hours = Mathf.FloorToInt(value / 3600);
            var minutes = Mathf.FloorToInt(value % 3600 / 60);
            var seconds = Mathf.FloorToInt(value % 60);

            _resultTextMesh.text = string.Format(_resultTextMesh.text, hours, minutes, seconds, coins, deaths);
            
            _canvasGroupController.Show();
        }
        
        private void Start()
        {
            _confirmButton.onClick.AddListener(OnClick);
            _canvasGroupController.InstantHide();

            LevelEnd.SceneCompletedAction += Show;
        }

        private void OnClick()
        {
            _inputReader.SetUI();
            BroAudio.Stop(BroAudioType.All);
            FootstepsSoundManager.Instance.Stop();
            SceneManager.LoadScene(_menuScene);
        }

        private void OnDestroy()
        {
            LevelEnd.SceneCompletedAction -= Show;
            _confirmButton.onClick.RemoveAllListeners();
        }
    }
}