using YG;
using YG.Utils.LB;

namespace _Scripts.Managers
{
    public static class LeaderboardManager
    {
        private const string _COINS_LEADERBOARD = "testCoinsLB";
        private static int _score;

        public static void Init()
        {
            YandexGame.onGetLeaderboard += OnGetLB;
        }

        public static void Update(int balance)
        {
            YandexGame.GetLeaderboard(_COINS_LEADERBOARD, int.MaxValue, int.MaxValue, int.MaxValue,  "nonePhoto");

            if (_score >= balance)
                return;
            
            YandexGame.NewLeaderboardScores(_COINS_LEADERBOARD, balance);
        }

        public static void Dispose()
        {
            YandexGame.onGetLeaderboard -= OnGetLB;
        }

        private static void OnGetLB(LBData data)
        {
            if (data.entries == "no data")
                return;
            
            _score = data.thisPlayer.score;
        }
    }
}