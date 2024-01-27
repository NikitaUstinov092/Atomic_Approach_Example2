using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;

namespace GamePlay.AI
{
    public class AutoAttackMechanics: IUpdate
    {
        private AtomicVariable<bool> _autoMode;
        private AtomicVariable<Transform> _heroTransform;
        private AtomicVariable<Entity.Entity> _targetEnemy;
        private AtomicVariable<float> _angleThreshold;
        private AtomicVariable<Vector3> _enemyDirection;
        private AtomicEvent _shootRequest;
        
        public void Construct(AtomicVariable<bool> autoMode, AtomicVariable<Transform> heroTransform, 
            AtomicEvent shootRequest, AtomicVariable<Entity.Entity> targetEnemy, AtomicVariable<float> angleThreshold,
            AtomicVariable<Vector3> enemyDirection)
        {
            _autoMode = autoMode;
            _heroTransform = heroTransform;
            _shootRequest = shootRequest;
            _targetEnemy = targetEnemy;
            _angleThreshold = angleThreshold;
            _enemyDirection = enemyDirection;
        }


        void IUpdate.Update(float deltaTime)
        {
           if(!_autoMode.Value || _targetEnemy.Value == null)
               return;

           if (_targetEnemy.Value.TryGet(out IGetHealth hp))
           {
               if (hp.GetHealth().Value < 0)
               {
                   return;
               }
           }
           
           var positionEnemy =  _targetEnemy.Value.transform.position - _heroTransform.Value.position;
            
           var directionToEnemy = positionEnemy.normalized;
          
           _enemyDirection.Value = directionToEnemy;
           
           var heroForward = _heroTransform.Value.forward;
           
           var dotProduct = Vector3.Dot(directionToEnemy, heroForward);
           
           if (!(dotProduct > _angleThreshold.Value)) 
                return;
            
           _shootRequest.Invoke();
        }
    }
}