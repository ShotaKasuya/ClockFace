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

        private IObjectResolver _objectResolver1;
        private IObjectResolver _objectResolver2;
        
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

            //Question: ここでusingステートメント使ってる理由が知りたいな...
            //containerBuilderのDisposeを呼んじゃうと、Builderに登録されてるクラスのDisposeも呼ばれちゃうみたいなんだよね。（挙動から推測して）
            //ClockHandRotationLogicはこの先（時間的に）も動作する（例: FixedTick）と思うから、ここでDisposeを呼んじゃうのはまずいんじゃないかな？
            //とりあえず、↑2行の推測が正しかった場合の修正案書いときます。
            _objectResolver1 = containerBuilder1.Build();
            _objectResolver2 = containerBuilder2.Build();
            
            var entity1 = _objectResolver1.Resolve<ClockHandEntity>();
            var entity2 = _objectResolver2.Resolve<ClockHandEntity>();
            var logic1 = _objectResolver1.Resolve<ClockHandRotationLogic>();
            var logic2 = _objectResolver2.Resolve<ClockHandRotationLogic>();

            _clockHandController = new ClockHandController(entity1, logic1, entity2, logic2);
            _gameManager = new GameManager(crystalController, _clockHandController);

            Debug.Log($"logic1");
            logic1.Initialize();
            Debug.Log($"logic2");
            logic2.Initialize();
        }
        
        private void FixedUpdate()
        {
            _clockHandController.CallTick();
        }

        private void OnDestroy()
        {
            _objectResolver1?.Dispose();
            _objectResolver2?.Dispose();
            _gameManager.Dispose();
        }
    }
}