using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.InGameScene.Cristal
{
    public static class CrystalBuilder
    {
        public static int[] BuildCrystals()
        {
            var length = DifficultySaver.GetLength();
            var arrayToReturn = new int[length];
            var cursor = Random.Range(0, length);

            for (int i = 0; i < length; i++)
            {
                var emptyIndexes = GetEmptyIndexes(arrayToReturn);
                var cursorNextTo = ChoseNext(cursor, arrayToReturn, emptyIndexes);

                switch (RandomDirection())
                {
                    case RotateDirection.Clockwise:
                        if (cursor > cursorNextTo)
                        {
                            arrayToReturn[cursor] = cursor - cursorNextTo;
                        }
                        else
                        {
                            arrayToReturn[cursor] = cursor + (length - cursorNextTo);
                        }
                        break;
                    case RotateDirection.CounterClockwise:
                        if (cursor>cursorNextTo)
                        {
                            arrayToReturn[cursor] = cursorNextTo + (length - cursor);
                        }
                        else
                        {
                            arrayToReturn[cursor] = cursor - cursorNextTo;
                        }
                        break;
                }

                Debug.Log($"cursor: {cursor}\n" +
                          $"cursorNextTo: {cursorNextTo}\n" +
                          $"set number: {arrayToReturn[cursor]}");
                cursor = cursorNextTo;
            }

            return arrayToReturn;
        }

        private static int[] GetEmptyIndexes(int[] array)
        {
            var arrayToReturn = new int[array.Count(x => x == 0)];

            var counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    arrayToReturn[counter] = i;
                    counter++;
                }
            }

            return arrayToReturn;
        }

        private static int ChoseNext(int cursor, int[] array, int[] emptyIndex)
        {
            if (emptyIndex.Length == 1) // Last
            {
                var tmp = Random.Range(0, array.Length-1);
                if (tmp >= cursor)
                {
                    return tmp + 1;
                }

                return tmp;
            }
            
            var cursorNext = Random.Range(0, emptyIndex.Length - 1);

            if (cursorNext < cursor)
            {
                return emptyIndex[cursorNext];
            }

            return emptyIndex[cursorNext + 1];
        }

        public static int[] BuildCrystalsTemp()
        {
            return new int[] { 1, 1, 2, 1, 1 };
        }

        private static RotateDirection RandomDirection()
        {
            switch (Random.Range(0,1))
            {
                case 0:
                    return RotateDirection.Clockwise;
                case 1:
                    return RotateDirection.CounterClockwise;
            }

            return RotateDirection.Clockwise;
        }
        
        private enum RotateDirection
        {
            Clockwise,CounterClockwise
        }
    }
}