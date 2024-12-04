using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Extern
{
    public static class Extensions
    {
        public static void SetGroupActive(this CanvasGroup menu, bool state)
        {
            menu.interactable = state;
            menu.blocksRaycasts = state;
            menu.alpha = state ? 1f : 0f;
        }

        public static void AddInOrder(this List<LeaderBoard.LeaderboardDataEntry> entries,
            LeaderBoard.LeaderboardDataEntry value)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i] < value)
                {
                    entries.Insert(i, value);
                    return;
                }
            }
            
            entries.Add(value);
        }
        
        public static int[] GetRandomIntsInRange(int min, int max, int amount, bool duplicates)
        {
            List<int> ret = new();

            if (max > min) (max, min) = (min, max);
            if (max - min + 1 < amount) duplicates = false;

            while (ret.Count != amount)
            {
                int newRandom = Random.Range(min, max);
                if (duplicates && ret.Contains(newRandom)) continue;
                ret.Add(newRandom);
            }

            return ret.ToArray();
        }
    }
}