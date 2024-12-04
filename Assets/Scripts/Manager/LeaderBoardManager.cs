using Data;
using UnityEngine;

namespace Manager
{
    public class LeaderBoardManager : MonoBehaviour
    {
        private const string LeaderboardDataKey = "LeaderboardData";
        private LeaderBoard _leaderBoard;
        [SerializeField] private int showNTopPlayers;
        private static LeaderBoardManager _instance;

        private void Awake()
        {
            if (_instance is not null)
            {
                Debug.LogWarning("More than one instance of LeaderBoardManager");
                Destroy(this);
                return;
            }
            _instance = this;
            
            string json = PlayerPrefs.GetString(LeaderboardDataKey, null);
            _leaderBoard = string.IsNullOrEmpty(json) ? new LeaderBoard() : LeaderBoard.FromJson(json);
        }
    
        public static void AddToBoard(string pName, int amount,  float accuracy)
        {
            _instance._leaderBoard.AddToLeaderBoard(pName, amount, accuracy);
            
            //Save
            string json = _instance._leaderBoard.ToJson();
            PlayerPrefs.SetString(LeaderboardDataKey, json);
        }

        public static LeaderBoard.LeaderboardDataEntry[] GetTop() => _instance._leaderBoard.GetTopN(_instance.showNTopPlayers);
    }
}
