using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    [Serializable]
    public sealed class RotateInDirectionEngine : IUpdate
    {
        private Transform _targetTransform;
        private AtomicVariable<float> _speed;

        private Vector3 _direction;

        public void Construct(Transform targetTransform, AtomicVariable<float> speed)
        {
            _targetTransform = targetTransform;
            _speed = speed;
        }
        void IUpdate.Update(float deltaTime)
        { 
            if (_direction == Vector3.zero)
                return;
            
            var currentRotation = _targetTransform.rotation;
            var targetRotation = Quaternion.LookRotation(_direction);
            _targetTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _speed * deltaTime);
        }
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
}