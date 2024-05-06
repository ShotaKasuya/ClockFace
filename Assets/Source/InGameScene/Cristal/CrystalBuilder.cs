using System.Linq;
using Source.InGameScene.ClockHand;
using Random = UnityEngine.Random;

namespace Source.InGameScene.Cristal
{
    public static class CrystalBuilder
    {
        public static int[] BuildCrystals()
        {
            var crystalLength = DifficultySaver.GetCrystalLength();
            var crystalsNumbers = new int[crystalLength];
            var cursor = Random.Range(0, crystalLength);

            for (int i = 0; i < crystalLength; i++)
            {
                var emptyIndexes = GetEmptyIndexes(crystalsNumbers);
                var cursorNextTo = ChoseNext(cursor, crystalsNumbers, emptyIndexes);

                //MEMO: ごめん！ここのロジックはちょっとよく分かんないのでレビュースキップします！！
                switch (RandomDirection())
                {
                    case RotateDirection.Clockwise:
                        if (cursor > cursorNextTo)
                        {
                            crystalsNumbers[cursor] = cursor - cursorNextTo;
                        }
                        else
                        {
                            crystalsNumbers[cursor] = cursor + (crystalLength - cursorNextTo);
                        }
                        break;
                    case RotateDirection.CounterClockwise:
                        if (cursor>cursorNextTo)
                        {
                            crystalsNumbers[cursor] = cursorNextTo + (crystalLength - cursor);
                        }
                        else
                        {
                            crystalsNumbers[cursor] = cursor - cursorNextTo;
                        }
                        break;
                }

                // Debug.Log($"cursor: {cursor}\n" +
                //           $"cursorNextTo: {cursorNextTo}\n" +
                //           $"set number: {arrayToReturn[cursor]}");
                cursor = cursorNextTo;
            }

            return crystalsNumbers;
        }

        private static int[] GetEmptyIndexes(int[] array)
        {
            var notUsedArray = new int[array.Count(x => x == 0)];

            var counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    notUsedArray[counter] = i;
                    counter++;
                }
            }

            return notUsedArray;
        }

        private static int ChoseNext(int cursor, int[] crystalNumbers, int[] emptyIndexes)
        {
            //MEMO: ごめん！ここのロジックはちょっとよく分かんないのでレビュースキップします！！
            if (emptyIndexes.Length == 1) // Last
            {
                //Advise: tmpだと意味が分からないので、変数名はちゃんと考えた方が良き。分かりづらい変数名しか思いつかないのであれば、変数の意味についてコメントを残すのも手。
                var tmp = Random.Range(0, crystalNumbers.Length-1);
                if (tmp >= cursor)
                {
                    return tmp + 1;
                }

                return tmp;
            }
            
            var cursorNext = Random.Range(0, emptyIndexes.Length - 1);

            if (emptyIndexes[cursorNext] < cursor)
            {
                return emptyIndexes[cursorNext];
            }

            return emptyIndexes[cursorNext + 1];
        }

        public static int[] BuildCrystalsTemp()
        {
            return new int[] { 1, 1, 2, 1, 1 };
        }

        private static RotateDirection RandomDirection()
        {
            //Advise: switchはこういう書き方もあるよ！
            return Random.Range(0, 1) switch //Question: これ0しか出力されなくない？
            {
                0 => RotateDirection.Clockwise,
                1 => RotateDirection.CounterClockwise,
                _ => RotateDirection.Clockwise
            };
        }
    }
}