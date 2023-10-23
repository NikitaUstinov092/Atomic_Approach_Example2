using System;
using System.Atomic.Implementations;

namespace GamePlay.Custom.Sections
{
    [Serializable]
    public sealed class TargetEntitySection
    {
        public AtomicVariable<Entity.Entity> TargetEntity;
    }
}

