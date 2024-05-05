using System;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Source.InGameScene.ClockHand
{
    public class ClockHandRotationLogic : IInitializable,IDisposable
    {
        private readonly ClockHandView _view;
        private readonly ClockHandEntity _entity;

        //Advise: どのタイミングで初期化されるか分からないので、コンストラクタで初期化するのがオススメ！
        private readonly ReactiveProperty<float> _rotateDegree;
        private const float ALLOWABLE_ERROR = 5.0f;

        [Inject]
        public ClockHandRotationLogic(ClockHandView clockHandView, ClockHandEntity clockHandEntity)
        {
            _view = clockHandView;
            _entity = clockHandEntity;
            _rotateDegree = new ReactiveProperty<float>(0f);
        }

        public void Initialize()
        {
            _entity.Cursor.Subscribe(x =>
            {
                //Advise: これならGameManagerではなく、Entityにデータ用クラスを作ってそこにCrystalAmountを持たせた方が良き。
                //Advise: 同じ計算式が2つあるので、1回にまとめた方が良き。
                float theta = x * Mathf.PI / GameManager.Instance.CrystalAmount * 2;
                Vector2 lookDir = new(Mathf.Cos(theta), Mathf.Sin(theta));
                
                //Advise: tmpだと意味が分からないので、変数名はちゃんと考えた方が良き。
                var rotateDegree = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                if (rotateDegree < 0)
                {
                    rotateDegree += 360f;
                }
                
                //Advise: ↑結構色んな計算してるけど、この式だけで完結するよ。
                float rotateDegree2 = 360f / GameManager.Instance.CrystalAmount * x;
                
                //Advise: Directionは向き（Vector2）なので、AngleとかDegreeとか最後につけるといいよ～
                _rotateDegree.Value = rotateDegree;
            });
        }

        public bool IsCompleted()
        {
            return Mathf.Approximately(_rotateDegree.Value, _view.ModelTransform.eulerAngles.z);
        }

        //Question: ITickableを継承しなかった理由とかある？
        public void FixedTick()
        {
            float currentAngle = _view.ModelTransform.eulerAngles.z;
            float diff = Mathf.Abs(currentAngle - _rotateDegree.Value);
            float newAngle;
            if (diff < ALLOWABLE_ERROR)
            {
                newAngle = _rotateDegree.Value;
            }
            else if (_entity.Direction == RotateDirection.Clockwise)
            {
                newAngle = currentAngle + _entity.RotateSpeed * Time.fixedDeltaTime;
            }
            else
            {
                newAngle = currentAngle - _entity.RotateSpeed * Time.fixedDeltaTime;
            }
            _view.ModelTransform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        public void Dispose()
        {
            _rotateDegree?.Dispose();
        }
    }
}