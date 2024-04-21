using System;
using R3;
using UnityEngine;

namespace Source.Title.View
{
    public class DifficultyCursorView: MonoBehaviour
    {
        public Transform ModelTransform
        {
            get;
            private set;
        }

        public Observable<InputKinds> InputEvent
        {
            get;
            private set;
        }

        private void Awake()
        {
            ModelTransform = transform;
            InputEvent = Observable.EveryUpdate(destroyCancellationToken)
                .ThrottleFirst(TimeSpan.FromSeconds(0.1))
                .Select(_ =>
                {
                    var up = Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow);
                    var down = Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow);
                    if (!(up ^ down))
                    {
                        return InputKinds.None;
                    }

                    if (up)
                    {
                        return InputKinds.Up;
                    }

                    return InputKinds.Down;
                });
        }
    }
}