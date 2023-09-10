using System;
using System.Atomic.Implementations;
using System.Atomic.Interfaces;

namespace GamePlay.Custom.Engines
{
    [Serializable]
    public class TakeDamageEngine: IAtomicAction<int>
    {
            private AtomicVariable<int> _hitPoints;
            private AtomicEvent<int> _takeDamageEvent;
            private AtomicVariable<bool> _isDead;
            private AtomicEvent _deathEvent;
           
            public void Use( 
                AtomicVariable<int> hitPoints,
                AtomicEvent<int> takeDamageEvent,
                AtomicVariable<bool> isDead, 
                AtomicEvent deathEvent)
            {
                _hitPoints = hitPoints;
                _takeDamageEvent = takeDamageEvent;
                _isDead = isDead;
                _deathEvent = deathEvent;
            }
            public void Invoke(int damage)
            {
                _hitPoints.Value -= damage;

                if (_hitPoints.Value > 0)
                {
                    _takeDamageEvent.Invoke(damage);
                    return;
                }
                        
                _isDead.Value = true;
                _deathEvent?.Invoke();
                
            }
    }
}
