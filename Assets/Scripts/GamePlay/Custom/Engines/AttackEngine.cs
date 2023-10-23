using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Components.Interfaces;

namespace GamePlay.Custom.Engines
{
    public class AttackEngine : ILateUpdate
    {
        public AtomicVariable<float> AttackDelay;
        public AtomicVariable<int> Damage;
        public AtomicVariable<Entity.Entity> Target;
    
        private bool _attack;
    
        private float _timer;
    
        public void Construct(AtomicVariable<float> attackDelay, AtomicVariable<int> damage, AtomicVariable<Entity.Entity> target)
        {
            AttackDelay = attackDelay;
            Damage = damage;
            Target = target;
        }
   
        void ILateUpdate.LateUpdate(float deltaTime)
        {
            if(!_attack)
                return;
                      
            _timer += deltaTime;
                        
            if (!(_timer >= AttackDelay.Value || !_attack)) 
                return;
                        
            if (Target.Value != null && 
                Target.Value.TryGet(out ITakeDamageable damage))
                damage.TakeDamage(Damage.Value);
                        
            _timer = 0f;
        }

        public void Attack()
        {
            _attack = true;
        }
    
        public void StopAttack()
        {
            _attack = false;
        }
    }
}
