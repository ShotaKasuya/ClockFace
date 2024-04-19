using System;
using Source.InGameScene.ClockHand;
using Source.InGameScene.Cristal;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.InGameScene
{
    public class InGameContainer: MonoBehaviour
    {
        [SerializeField] private CrystalController crystalController;
        [SerializeField] private ClockHandView clockHandView1;
        [SerializeField] private ClockHandView clockHandView2;

        private GameManager _gameManager;
        private ClockHandController _clockHandController;
        
        
        private void Awake()
        {
            var containerBuilder1 = new ContainerBuilder();
            containerBuilder1.RegisterComponent(clockHandView1);
            containerBuilder1.Register(_ => new ClockHandEntity(RotateDirection.Clockwise), Lifetime.Scoped);
            containerBuilder1.Register<ClockHandRotationLogic>(Lifetime.Singleton);

            var containerBuilder2 = new ContainerBuilder();
            containerBuilder2.RegisterComponent(clockHandView2);
            containerBuilder2.Register(_ => new ClockHandEntity(RotateDirection.CounterClockwise), Lifetime.Scoped);
            containerBuilder2.Register<ClockHandRotationLogic>(Lifetime.Singleton);

            using IObjectResolver objectResolver1 = containerBuilder1.Build();
            using IObjectResolver objectResolver2 = containerBuilder2.Build();
            {
                var entity1 = objectResolver1.Resolve<ClockHandEntity>();
                var entity2 = objectResolver2.Resolve<ClockHandEntity>();
                var logic1 = objectResolver1.Resolve<ClockHandRotationLogic>();
                var logic2 = objectResolver2.Resolve<ClockHandRotationLogic>();

                _clockHandController = new ClockHandController(entity1, logic1, entity2, logic2);
                _gameManager = new GameManager(crystalController, _clockHandController);
            }
        }
        
        private void FixedUpdate()
        {
            _clockHandController.CallTick();
        }

        private void OnDestroy()
        {
            _gameManager.Dispose();
        }
    }
}