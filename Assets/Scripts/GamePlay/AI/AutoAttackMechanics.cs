using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Custom.Engines;
using GamePlay.Hero;
using UnityEngine;

namespace GamePlay.AI
{
    public class AutoAttackMechanics: IUpdate
    {
        private AtomicVariable<bool> _autoMode;
        private AtomicVariable<Transform> _heroTransform;
        private AtomicEvent _shootRequest;
        private AtomicVariable<Entity.Entity> _targetEnemy;
        private RotateInDirectionMechanics _rotateInDirectionMechanics;
        
        public void Construct(AtomicVariable<bool> autoMode, AtomicVariable<Transform> heroTransform, 
            AtomicEvent shootRequest, AtomicVariable<Entity.Entity> targetEnemy, 
            RotateInDirectionMechanics rotateInDirectionMechanics)
        {
            _autoMode = autoMode;
            _heroTransform = heroTransform;
            _shootRequest = shootRequest;
            _targetEnemy = targetEnemy;
            _rotateInDirectionMechanics = rotateInDirectionMechanics;
        }


        void IUpdate.Update(float deltaTime)
        {
           if(!_autoMode.Value || _targetEnemy.Value == null)
               return;
           var positionEnemy =  _targetEnemy.Value.transform.position - _heroTransform.Value.position;
           _rotateInDirectionMechanics.SetDirection(positionEnemy);
           _shootRequest.Invoke();
        }
    }
}