using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Title.Logic
{
    public class MoveSinVerticalLogic: ITickable
    {
        //Advise: currentはちょっと分かりづらいかな。
        private float _currentTime = 0f;
        //Advise: speedだと諸々計算して最終的に決まった速度というイメージがある...(個人の感想です)
        private readonly float _speedRate = 5f;
        private readonly IGettableTransform _target;

        public void Tick()
        {
            _target.ModelTransform.Translate(new Vector3(0f, Mathf.Cos(_currentTime * _speedRate)));
            _currentTime += Time.deltaTime;
        }

        [Inject]
        public MoveSinVerticalLogic(IGettableTransform gettableTransform)
        {
            _target = gettableTransform;
        }
    }
}