using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using Source.InGameScene.ClockHand;
using Source.InGameScene.Cristal;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;


namespace Source.InGameScene
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CrystalController crystalController;
        [SerializeField] private ClockHandView clockHandView1;
        [SerializeField] private ClockHandView clockHandView2;
        private ClockHandController _clockHandController;
        
        private static GameManager _gameManager;
        public static GameManager Instance => _gameManager;
        public int crystalAmount;
        
        

        private Queue<(int, int)> _cursorTask;
        
        private void Awake()
        {
            _gameManager = this;

            var containerBuilder1 = new ContainerBuilder();
            containerBuilder1.RegisterComponent(clockHandView1);
            containerBuilder1.Register<ClockHandEntity>(Lifetime.Singleton);
            containerBuilder1.Register<ClockHandRotationLogic>(Lifetime.Singleton);

            var containerBuilder2 = new ContainerBuilder();
            containerBuilder2.RegisterComponent(clockHandView2);
            containerBuilder2.Register<ClockHandEntity>(Lifetime.Singleton);
            containerBuilder2.Register<ClockHandRotationLogic>(Lifetime.Singleton);

            using IObjectResolver objectResolver1 = containerBuilder1.Build();
            using IObjectResolver objectResolver2 = containerBuilder2.Build();
            {
                var entity1 = objectResolver1.Resolve<ClockHandEntity>();
                var entity2 = objectResolver2.Resolve<ClockHandEntity>();
                var logic1 = objectResolver1.Resolve<ClockHandRotationLogic>();
                var logic2 = objectResolver2.Resolve<ClockHandRotationLogic>();

                _clockHandController = new ClockHandController(entity1, logic1, entity2, logic2);
            }
        }

        private void ClearEvent()
        {
            Debug.Log("Clear");
        }

        private void FailEvent()
        {
            Debug.Log("Fail");
        }

        private void FixedUpdate()
        {
            _clockHandController.CallTick();
        }

        public async void SetHand(int index, int number)
        {
            if (!crystalController.CanDisable(index)) return;
            if (!_clockHandController.CanRotate(index)) return;
            
            _clockHandController.SetHand(index, number);

            await UniTask.WaitUntil(_clockHandController.IsCompleted);
            crystalController.DisableCrystal(index);
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
            var isCan1 = crystalController.CanDisable(cursor1);
            var isCan2 = crystalController.CanDisable(cursor2);
            // Debug.Log($"cursor1: {cursor1} : {isCan1}\n" +
            //           $"cursor2: {cursor2}: {isCan2}");
            
            if (crystalController.IsCleared())
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