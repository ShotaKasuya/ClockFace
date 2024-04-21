using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VContainer;

namespace Source.InGameScene.ClockHand
{
    public class ClockHandEntity
    {
        public readonly RotateDirection Direction;
        public readonly ReactiveProperty<int> Cursor = new ReactiveProperty<int>(0);
        private Vector2 _targetDirection;
        public readonly float RotateSpeed = 100f;

        public void SetCursor(int cursor)
        {
            Cursor.Value = cursor;
        }

        public ClockHandEntity(RotateDirection direction)
        {
            Direction = direction;
        }
    }
}