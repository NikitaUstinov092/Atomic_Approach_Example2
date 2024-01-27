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
       
        public AtomicEvent<int> TakeDamageEvent = new();
        public AtomicEvent DeathEvent = new();
        public AtomicEvent<Entity.Entity> DeathEventData = new();
        public AtomicVariable<Entity.Entity> Entity = new();
        public AtomicVariable<int> HitPoints = new();
        public AtomicVariable<bool> IsDead= new();
        
        private TakeDamageAction _takeDamageAction = new();
            
        [Construct]
        public void Construct()
        {
            TakeDamageRequest.Construct(damage =>
                {
                    if (IsDead.Value)
                        return;
                            
                    _takeDamageAction.Invoke(damage);
                }
            );
            _takeDamageAction.Construct(HitPoints, TakeDamageEvent, IsDead, DeathEvent, Entity, DeathEventData);
        }
    }
}