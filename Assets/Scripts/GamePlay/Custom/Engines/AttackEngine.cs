using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Components.Interfaces;

namespace GamePlay.Custom.Engines
{
    public class AttackEngine : ILateUpdate
    {
        private AtomicVariable<float> _attackDelay;
        private AtomicVariable<int> _damage;
        private AtomicVariable<Entity.Entity> _target;
        private AtomicVariable<bool> _attack;
        private AtomicVariable<float> _timer;
    
        public void Construct(AtomicVariable<float> attackDelay, AtomicVariable<int> damage, AtomicVariable<Entity.Entity> target, 
            AtomicVariable<bool> attack, AtomicVariable<float> timer)
        {
            _attackDelay = attackDelay;
            _damage = damage;
            _target = target;
            _attack = attack;
            _timer = timer;
        }
   
        void ILateUpdate.LateUpdate(float deltaTime)
        {
            if(!_attack.Value)
                return;
                      
            _timer.Value += deltaTime;
                        
            if (!(_timer.Value >= _attackDelay.Value || !_attack.Value)) 
                return;
                        
            if (_target.Value != null && 
                _target.Value.TryGet(out ITakeDamageable damage))
                damage.TakeDamage(_damage.Value);
                        
            _timer.Value = 0f;
        }

        public void Attack()
        {
            _attack.Value = true;
        }
    
        public void StopAttack()
        {
            _attack.Value = false;
        }
    }
}
