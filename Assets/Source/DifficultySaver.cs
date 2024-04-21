using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public static class DifficultySaver
    {
        public static Difficulty CurrentDifficulty = Difficulty.Normal;

        public static void MoreHard()
        {
            if (CurrentDifficulty >= Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().Max())
            {
                CurrentDifficulty = Difficulty.Easy;
                return;
            }

            CurrentDifficulty++;
        }
        
        public static void MoreEasy()
        {
            if (CurrentDifficulty <= Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().Min())
            {
                CurrentDifficulty = Difficulty.Hard;
                return;
            }

            CurrentDifficulty--;
        }


        
        public static int GetLength()
        {
            switch (CurrentDifficulty)
            {
                case Difficulty.Easy:
                    return Random.Range(4, 6);
                case Difficulty.Normal:
                    return Random.Range(6, 9);
                case Difficulty.Hard:
                    return Random.Range(9, 14);
                
            }

            Debug.LogError("It shouldn't arrive");
            return 0;
        }
    }
}