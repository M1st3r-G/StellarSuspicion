using System;
using System.Collections.Generic;
using Extern;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LeaderBoard
    {
        public List<LeaderboardDataEntry> leaderboardDataEntries = new();
    
        public const int MaxEntries = 10;
        
        public string ToJson() => JsonUtility.ToJson(this);
        public static LeaderBoard FromJson(string json) => JsonUtility.FromJson<LeaderBoard>(json);

        public void AddToLeaderBoard(string name, int numOfCreatures, float accuracy)
        {
            leaderboardDataEntries.AddInOrder(new LeaderboardDataEntry(name, accuracy, numOfCreatures));
            if (leaderboardDataEntries.Count > MaxEntries) leaderboardDataEntries.RemoveAt(MaxEntries);
        }
    
        public LeaderboardDataEntry[] GetTopN(int n) => 
            leaderboardDataEntries.GetRange(0, leaderboardDataEntries.Count < n ? leaderboardDataEntries.Count : n).ToArray();

        [Serializable]
        public readonly struct LeaderboardDataEntry
        {
            public readonly string Name;
            public readonly int NumberOfCreatures;
            public readonly float Accuracy;

            private int Order => (int)(NumberOfCreatures * Accuracy);
            
            public LeaderboardDataEntry(string name, float accuracy, int numberOfCreatures)
            {
                Name = name;
                Accuracy = accuracy;
                NumberOfCreatures = numberOfCreatures;
            }

            public static bool operator <(LeaderboardDataEntry a, LeaderboardDataEntry b) => a.Order < b.Order;
            public static bool operator >(LeaderboardDataEntry a, LeaderboardDataEntry b) => a.Order > b.Order;
        }
        
    }
}