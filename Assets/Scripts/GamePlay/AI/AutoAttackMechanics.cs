using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Custom.Engines;
using GamePlay.Hero;

namespace GamePlay.AI
{
    public class AutoAttackMechanics: IUpdate
    {
        private AtomicVariable<bool> _autoMode;
        private AtomicVariable<HeroModel> _hero;
        private AtomicEvent _shootRequest;
        private AtomicVariable<Entity.Entity> _targetEnemy;
        private RotateInDirectionMechanics _rotateInDirectionMechanics;
        
        public void Construct(AtomicVariable<bool> autoMode, AtomicVariable<HeroModel> hero, 
            AtomicEvent shootRequest, AtomicVariable<Entity.Entity> targetEnemy, 
            RotateInDirectionMechanics rotateInDirectionMechanics)
        {
            _autoMode = autoMode;
            _hero = hero;
            _shootRequest = shootRequest;
            _targetEnemy = targetEnemy;
            _rotateInDirectionMechanics = rotateInDirectionMechanics;
        }


        void IUpdate.Update(float deltaTime)
        {
           if(!_autoMode.Value || _targetEnemy.Value == null)
               return;
           
           _rotateInDirectionMechanics.SetDirection(_targetEnemy.Value.transform.position);
           _shootRequest.Invoke();
        }
    }
}