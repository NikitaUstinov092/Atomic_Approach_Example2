using System;
using System.Atomic.Implementations;

[Serializable]
    public sealed class TargetEntitySection
    {
        public AtomicVariable<Entity.Entity> TargetEntity;
    }

