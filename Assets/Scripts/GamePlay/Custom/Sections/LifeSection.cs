using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Engines;

namespace GamePlay.Custom.Sections
{
    [Serializable]
    public class LifeSection
    {
        public AtomicAction<int> TakeDamageRequest = new();
        public TakeDamageEngine TakeDamageEngine = new();
        public AtomicEvent<int> TakeDamageEvent = new();
                
        public AtomicEvent DeathEvent = new();
        public AtomicVariable<int> HitPoints = new();
        public AtomicVariable<bool> IsDead= new();
            
        [Construct]
        public void Construct()
        {
            TakeDamageRequest.Use(damage =>
                {
                    if (IsDead.Value)
                        return;
                            
                    TakeDamageEngine.Invoke(damage);
                }
            );
            TakeDamageEngine.Use(HitPoints, TakeDamageEvent,IsDead,DeathEvent);
        }
    }
}