using System;
using System.Atomic.Implementations;
using UnityEngine;

namespace Lesson
{
    [Serializable]
    public sealed class MovementDirectionVariable : AtomicVariable<Vector3>
    {
        public AtomicEvent MovementStarted { get; set; } = new();
        public AtomicEvent MovementFinished { get; set; } = new();

        public void SetValue(Vector3 value)
        {
            var previousValue = Value;
            Value = value;

            var isPreviousValueZero = previousValue == Vector3.zero;
            var isCurrentValueZero = Value == Vector3.zero;

            switch (isPreviousValueZero)
            {
                case true when !isCurrentValueZero:
                    MovementStarted?.Invoke();
                    break;
                case false when isCurrentValueZero:
                    MovementFinished?.Invoke();
                    break;
            }
        }
    }
}
