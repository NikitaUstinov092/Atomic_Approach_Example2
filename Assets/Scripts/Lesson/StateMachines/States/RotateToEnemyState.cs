using Lessons.Character.Engines;
using Lessons.StateMachines.States;
using UnityEngine;

namespace Lesson.StateMachines.States
{
    public class RotateToEnemyState : UpdateState
    {
        private TargetEntitySection _entitySection;
        private RotateInDirectionEngine _rotateInDirection;
        private Vector3 _positionEnemy;
        public void Construct(TargetEntitySection entitySection,
            RotateInDirectionEngine rotateInDirection)
        {
            _rotateInDirection = rotateInDirection;
            _entitySection = entitySection;
        }
        protected override void OnEnter()
        {
            _entitySection.TargetEntity.Subscribe(SetDirection);
            SetDirection(_entitySection.TargetEntity.Value);
        }
        protected override void OnExit()
        {
            _entitySection.TargetEntity.Unsubscribe(SetDirection);
            _rotateInDirection.SetDirection(Vector3.zero);
        }
        protected override void OnUpdate(float deltaTime)
        {
            _rotateInDirection.SetDirection(_positionEnemy);
        }
        private void SetDirection(Entity.Entity entity)
        {
            if (entity != null)
                _positionEnemy = entity.transform.position;
        }
    }
}
