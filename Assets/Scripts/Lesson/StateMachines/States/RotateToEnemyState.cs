using Lessons.Character.Engines;
using Lessons.StateMachines.States;
using UnityEngine;

namespace Lesson.StateMachines.States
{
    public class RotateToEnemyState : IState
    {
        private TargetEntitySection _entitySection;
        private RotateInDirectionEngine _rotateInDirection;
        private Transform _heroTransform;
        private Vector3 _positionEnemy;
        public void Construct(TargetEntitySection entitySection, Transform heroTransform,
            RotateInDirectionEngine rotateInDirection)
        {
            _rotateInDirection = rotateInDirection;
            _entitySection = entitySection;
            _heroTransform = heroTransform;
        }
        
        void IState.Enter()
        {
            _entitySection.TargetEntity.Subscribe(SetDirection);
            SetDirection(_entitySection.TargetEntity.Value);
        }

        void IState.Exit()
        {
            _entitySection.TargetEntity.Unsubscribe(SetDirection);
            _rotateInDirection.SetDirection(Vector3.zero);
        }
        
        private void SetDirection(Entity.Entity entity)
        {
            if (entity == null) 
                return;
            
            _positionEnemy =  entity.transform.position - _heroTransform.position;
            _rotateInDirection.SetDirection(_positionEnemy);
        }
    }
}
