using System;
using _Scripts.Player;
using _Scripts.Utilities;

namespace _Scripts.Managers
{
    public class ReloadRoomManager : Singleton<ReloadRoomManager>
    {
        public static Action ReloadRoomAction;
        
        private void Start()
        {
            PlayerController.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            ReloadRoomAction?.Invoke();
        }

        private void OnDestroy()
        {
            PlayerController.PlayerDiedAction -= OnPlayerDied;
        }
    }
}