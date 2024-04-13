using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.InGameScene.ClockHand
{
    public class ClockHandController
    {
        private bool _firstRotationIsCalled = false;
        
        private readonly ClockHandEntity _clockHandEntity1;
        private readonly ClockHandRotationLogic _clockHandRotationLogic1;

        private readonly ClockHandEntity _clockHandEntity2;
        private readonly ClockHandRotationLogic _clockHandRotationLogic2;
        

        [Inject]
        public ClockHandController(ClockHandEntity clockHandEntity1, ClockHandRotationLogic clockHandRotationLogic1,
            ClockHandEntity clockHandEntity2, ClockHandRotationLogic clockHandRotationLogic2)
        {
            _clockHandEntity1 = clockHandEntity1;
            _clockHandEntity2 = clockHandEntity2;
            _clockHandRotationLogic1 = clockHandRotationLogic1;
            _clockHandRotationLogic2 = clockHandRotationLogic2;
            // _clockHandEntity1.Cursor.Subscribe(x => Debug.Log($"Set to 1 : {x}"));
            // _clockHandEntity2.Cursor.Subscribe(x => Debug.Log($"Set to 2 : {x}"));
        }

        public (int, int) GetClockHandCursor()
        {
            return (_clockHandEntity1.Cursor.Value, _clockHandEntity2.Cursor.Value);
        }

        public bool IsCompleted()
        {
            return _clockHandRotationLogic1.IsCompleted() & _clockHandRotationLogic2.IsCompleted();
        }

        public void CallTick()
        {
            _clockHandRotationLogic1.FixedTick();
            _clockHandRotationLogic2.FixedTick();
        }

        public bool CanRotate(int index)
        {
            if (!_firstRotationIsCalled)
            {
                return true;
            }
            if ((_clockHandEntity1.Cursor.Value == index || _clockHandEntity2.Cursor.Value == index) && IsCompleted())
            {
                return true;
            }

            return false;
        }

        private void SetCursors(int value1, int value2)
        {
            _clockHandEntity1.Cursor.Value = CheckOverUnderFlow(value1);
            _clockHandEntity2.Cursor.Value = CheckOverUnderFlow(value2);
        }
        
        public async void SetHand(int index, int number)
        {
            // Debug.Log($"called : {nameof(SetHand)}\n" +
            //           $"index : {index}, number : {number}");
            if (!_firstRotationIsCalled)
            {
                _firstRotationIsCalled = true;
                SetCursors(index, index);
                
                await UniTask.WaitUntil(IsCompleted);

                SetCursors(index + number, index - number);
                return;
            }
            
            if (!IsCompleted())
            {
                Debug.Log("Not Completed");
                return;
            }
            if (!(_clockHandEntity1.Cursor.Value == index || _clockHandEntity2.Cursor.Value == index))
            {
                Debug.Log($"Index Not Match : {index}\n" +
                          $"(value1, value2) : ({_clockHandEntity1.Cursor.Value}, {_clockHandEntity2.Cursor.Value})");
                return;
            }

            if (_clockHandEntity1.Cursor.Value == _clockHandEntity2.Cursor.Value)
            {
                SetCursors(index - number, index + number);
            }
            else
            {
                if (_clockHandEntity1.Cursor.Value == index)
                {
                    SetCursors(_clockHandEntity1.Cursor.Value, index);
                    await UniTask.WaitUntil(IsCompleted);
                    SetCursors(index + number, index - number);
                }
                else
                {
                    SetCursors(index, _clockHandEntity2.Cursor.Value);
                    await UniTask.WaitUntil(IsCompleted);
                    SetCursors(index + number, index - number);
                }
            }
        }

        private int CheckOverUnderFlow(int num)
        {
            if (num < 0)
            {
                return num + GameManager.Instance.CrystalAmount;
            }

            if (num >= GameManager.Instance.CrystalAmount)
            {
                return num % GameManager.Instance.CrystalAmount;
            }

            return num;
        }
        
    }
}
