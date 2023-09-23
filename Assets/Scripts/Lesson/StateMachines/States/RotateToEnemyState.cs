using System.Atomic.Implementations;
using GamePlay.Hero;
using Lessons.StateMachines.States;
using UnityEngine;

namespace Lesson.StateMachines.States
{
    public class RotateToEnemyState : UpdateState
    {
        private HeroModel_Core.TargetEnemyContainer _enemyContainer;
        private AtomicVariable<Transform> _sourceTransform;
        public void Construct(HeroModel_Core.TargetEnemyContainer enemyContainer,
            AtomicVariable<Transform> sourceTransform)
        {
            _enemyContainer = enemyContainer;
            _sourceTransform = sourceTransform;
        }
        protected override void OnUpdate(float deltaTime)
        {
            if (_enemyContainer.Enemy.Value == null)
                return;
            var enemyTransform = _enemyContainer.Enemy.Value.transform;
            _sourceTransform.Value.LookAt(enemyTransform);
        }
    }
}
