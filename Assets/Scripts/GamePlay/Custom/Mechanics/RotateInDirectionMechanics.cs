using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    [Serializable]
    public sealed class RotateInDirectionMechanics : IUpdate
    {
        private AtomicVariable<Transform> _targetTransform;
        private AtomicVariable<float> _speed;
        private AtomicVariable<Vector3> _direction;

        public void Construct(AtomicVariable<Transform>  targetTransform,AtomicVariable<Vector3> direction, AtomicVariable<float> speed)
        {
            _targetTransform = targetTransform;
            _speed = speed;
            _direction = direction;
        }
        void IUpdate.Update(float deltaTime)
        { 
            if (_direction.Value == Vector3.zero)
                return;
            
            var currentRotation = _targetTransform.Value.rotation;
            var targetRotation = Quaternion.LookRotation(_direction);
            _targetTransform.Value.rotation = Quaternion.Slerp(currentRotation, targetRotation, _speed.Value * deltaTime);
        }
        public void SetDirection(Vector3 direction)
        {
            _direction.Value = direction;
        }
    }
}