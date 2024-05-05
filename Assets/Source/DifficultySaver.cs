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
            //Advise: 下記MoreEasyメソッドのコメントを参照
            if (CurrentDifficulty >= Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().Max())
            {
                CurrentDifficulty = Difficulty.Easy;
                return;
            }

            CurrentDifficulty++;
        }
        
        public static void MoreEasy()
        {
            //Advise: CurrentDifficulty = Difficulty.Hard; って書いちゃうとDifficultyの中身に依存することになる。
            //折角if文の中はDifficultyの数に依存しないように工夫してるのに、この一文は依存してるのもったいないので、以下の書き方を推奨。
            if (CurrentDifficulty <= 0)
            {
                CurrentDifficulty = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().Max();
                return;
            }
            CurrentDifficulty--;
        }


        //Advise: 難しさ（DifficultySaverより）の長さ...？　という解釈にならないように修正。LengthじゃなくてCountでもいいかも。
        public static int GetCrystalLength()
        {
            //Advise: こういう書き方もあります。参考程度に...
            switch (CurrentDifficulty)
            {
                case Difficulty.Easy:
                    return Random.Range(4, 6);
                case Difficulty.Normal:
                    return Random.Range(6, 9);
                case Difficulty.Hard:
                    return Random.Range(9, 14);
                default:
                    Debug.LogError("It shouldn't arrive");
                    return 0;
            }
        }
    }
}