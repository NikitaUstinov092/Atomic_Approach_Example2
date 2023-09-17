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

        public void Construct(AtomicVariable<bool> moveRequired)
        {
            moveRequired.Subscribe((Value) =>
            {
                if(Value)
                    MovementStarted?.Invoke();
                else
                    MovementFinished.Invoke();
            });
        }
        
    }
}
