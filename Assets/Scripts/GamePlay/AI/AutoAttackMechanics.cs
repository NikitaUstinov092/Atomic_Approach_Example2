using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Custom.Engines;
using UnityEngine;

namespace GamePlay.AI
{
    public class AutoAttackMechanics: IUpdate
    {
        private AtomicVariable<bool> _autoMode;
        private AtomicVariable<Transform> _heroTransform;
        private AtomicVariable<Entity.Entity> _targetEnemy;
        private AtomicVariable<float> _dotProduct;
        private AtomicEvent _shootRequest;
        private RotateInDirectionMechanics _rotateInDirectionMechanics;
        
        public void Construct(AtomicVariable<bool> autoMode, AtomicVariable<Transform> heroTransform, 
            AtomicEvent shootRequest, AtomicVariable<Entity.Entity> targetEnemy, 
            RotateInDirectionMechanics rotateInDirectionMechanics,  AtomicVariable<float> dotProduct)
        {
            _autoMode = autoMode;
            _heroTransform = heroTransform;
            _shootRequest = shootRequest;
            _targetEnemy = targetEnemy;
            _rotateInDirectionMechanics = rotateInDirectionMechanics;
            _dotProduct = dotProduct;
        }


        void IUpdate.Update(float deltaTime)
        {
           if(!_autoMode.Value || _targetEnemy.Value == null)
               return;

           if (_targetEnemy.Value.TryGet(out IGetHealth hp))
           {
               if(hp.GetHealth().Value <= 0)
                   return;
           }
           
           var positionEnemy =  _targetEnemy.Value.transform.position - _heroTransform.Value.position;
           
           _rotateInDirectionMechanics.SetDirection(positionEnemy);
           
           var directionToEnemy = positionEnemy.normalized;
           var heroForward = _heroTransform.Value.forward;
           
           var dotProduct = Vector3.Dot(directionToEnemy, heroForward);

           if (!(dotProduct > _dotProduct.Value)) 
               return;
           
           _rotateInDirectionMechanics.SetDirection(directionToEnemy);
           _shootRequest.Invoke();
        }
    }
}