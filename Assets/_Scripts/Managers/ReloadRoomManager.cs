using System;
using _Scripts.Utilities;
using _Scripts.Utilities.StateMachine;

namespace _Scripts.Managers
{
    public class ReloadRoomManager : Singleton<ReloadRoomManager>
    {
        public static Action ReloadRoomAction;
        
        private void Start()
        {
            FsmDiedState.PlayerDiedAction += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            ReloadRoomAction?.Invoke();
        }

        private void OnDestroy()
        {
            FsmDiedState.PlayerDiedAction -= OnPlayerDied;
        }
    }
}