using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;


namespace Source.InGameScene.ClockHand
{
    public class ClockHandRotationLogic 
    {
        private readonly ClockHandView _view;
        private readonly ClockHandEntity _entity;

        private readonly ReactiveProperty<float> _rotateDirection = new ReactiveProperty<float>(0f);
        private const float ALLOWABLE_ERROR = 3.0f;

        [Inject]
        public ClockHandRotationLogic(ClockHandView clockHandView, ClockHandEntity clockHandEntity)
        {
            _view = clockHandView;
            _entity = clockHandEntity;
            clockHandEntity.Cursor.Subscribe(x =>
            {
                Vector2 lookDir = new(Mathf.Cos(x * Mathf.PI / GameManager.Instance.CrystalAmount * 2),
                    Mathf.Sin(x * Mathf.PI / GameManager.Instance.CrystalAmount * 2));
                var tmp = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                if (tmp < 0)
                {
                    tmp += 360f;
                }

                _rotateDirection.Value = tmp;
                // Debug.Log($"Set Cursor : {x}\n" +
                //           $"look dir : {lookDir}");
            });
            // _rotateDirection.Subscribe(x => Debug.Log($"{nameof(_rotateDirection)} : {x}"));
        }

        public bool IsCompleted()
        {
            return Mathf.Approximately(_rotateDirection.Value, _view.ModelTransform.eulerAngles.z);
        }

        public void FixedTick()
        {
            float currentAngle = _view.ModelTransform.eulerAngles.z;
            float diff = Mathf.Abs(currentAngle - _rotateDirection.Value);
            float newAngle;
            if (diff < ALLOWABLE_ERROR)
            {
                newAngle = _rotateDirection.Value;
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
    }
}