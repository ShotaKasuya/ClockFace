using System;
using UnityEngine;
using VContainer;

namespace Source.InGameScene
{
    public class TestMonoBehaviour: MonoBehaviour
    {
        private void Start()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register<Source.Logger>(Lifetime.Singleton);
            containerBuilder.Register<Calculator>(Lifetime.Singleton);
            containerBuilder.Register<HogeClass>(Lifetime.Singleton);

            using IObjectResolver objectResolver = containerBuilder.Build();

            HogeClass hoge = objectResolver.Resolve<HogeClass>();

            hoge.LoggerTest();
            hoge.CalculatorTest(3, 5);

        }
    }
}