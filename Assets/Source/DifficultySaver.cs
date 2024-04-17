using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public static class DifficultySaver
    {
        public static Difficulty Difficulty = Difficulty.Hard;

        public static int GetLength()
        {
            switch (Difficulty)
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