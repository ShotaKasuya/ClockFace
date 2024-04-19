using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Title
{
    public class MoveSinVerticalLogic: ITickable
    {
        private float _current = 0f;
        private readonly float _speed = 3f;
        private readonly IGettableTransform _target;

        public void Tick()
        {
            _target.ModelTransform.Translate(new Vector3(0f, Mathf.Cos(_current*_speed)));
            _current += Time.deltaTime;
        }

        [Inject]
        public MoveSinVerticalLogic(IGettableTransform gettableTransform)
        {
            _target = gettableTransform;
        }
    }
}