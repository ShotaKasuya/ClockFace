using Cysharp.Threading.Tasks;
using Source.InGameScene.ClockHand;
using Source.InGameScene.Cristal;
using UnityEngine;


namespace Source.InGameScene
{
    public class GameManager
    {
        private readonly CrystalController _crystalController;
        private readonly ClockHandController _clockHandController;
        
        private static GameManager _gameManager;
        public static GameManager Instance => _gameManager;
        public readonly int CrystalAmount;

        public GameManager(int crystalAmount, CrystalController crystalController, ClockHandController clockHandController)
        {
            _gameManager = this;
            CrystalAmount = crystalAmount;
            _clockHandController = clockHandController;
            _crystalController = crystalController;
        }

        private void ClearEvent()
        {
            Debug.Log("Clear");
        }

        private void FailEvent()
        {
            Debug.Log("Fail");
        }

        public async void SetHand(int index, int number)
        {
            if (!_crystalController.CanDisable(index)) return;
            if (!_clockHandController.CanRotate(index)) return;
            
            _clockHandController.SetHand(index, number);

            await UniTask.WaitUntil(_clockHandController.IsCompleted);
            _crystalController.DisableCrystal(index);
            switch (CheckGameEnd())
            {
                case GameUpdateEntity.Clear:
                    ClearEvent();
                    break;
                case GameUpdateEntity.Fail:
                    FailEvent();
                    break;
            }
        }

        private GameUpdateEntity CheckGameEnd()
        {
            var (cursor1, cursor2) = _clockHandController.GetClockHandCursor();
            var isCan1 = _crystalController.CanDisable(cursor1);
            var isCan2 = _crystalController.CanDisable(cursor2);
            // Debug.Log($"cursor1: {cursor1} : {isCan1}\n" +
            //           $"cursor2: {cursor2}: {isCan2}");
            
            if (_crystalController.IsCleared())
            {
                return GameUpdateEntity.Clear;
            }
            if (isCan1 || isCan2)
            {
                return GameUpdateEntity.Continue;
            }
            return GameUpdateEntity.Fail;
        }
    }
}