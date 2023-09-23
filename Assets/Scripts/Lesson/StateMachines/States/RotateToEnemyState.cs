using System.Atomic.Implementations;
using GamePlay.Hero;
using Lessons.StateMachines.States;
using UnityEngine;

namespace Lesson.StateMachines.States
{
    public class RotateToEnemyState : UpdateState
    {
        private TargetEntitySection _entitySection;
        private AtomicVariable<Transform> _sourceTransform;
        public void Construct(TargetEntitySection entitySection,
            AtomicVariable<Transform> sourceTransform)
        {
            _entitySection = entitySection;
            _sourceTransform = sourceTransform;
        }
        protected override void OnUpdate(float deltaTime)
        {
            var target = _entitySection.TargetEntity.Value;
            if (target == null)
                return;
            var enemyTransform = target.transform;
            _sourceTransform.Value.LookAt(enemyTransform);
        }
    }
}
