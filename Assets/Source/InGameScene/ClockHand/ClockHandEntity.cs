using System;
using R3;
using VContainer;

namespace Source.InGameScene.ClockHand
{
    public class ClockHandEntity:IDisposable
    {
        public RotateDirection Direction { get; }
        public ReactiveProperty<int> Cursor { get; }
        public float RotateSpeed => 100f;

        public void SetCursor(int cursor)
        {
            Cursor.Value = cursor;
        }

        //Question: [Inject]っていらないの？
        [Inject]
        public ClockHandEntity(RotateDirection direction)
        {
            Direction = direction;
            Cursor = new ReactiveProperty<int>(0);
        }

        public void Dispose()
        {
            Cursor?.Dispose();
        }
    }
}