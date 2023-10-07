using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;


    [Serializable]
    public sealed class MoveInDirectionEngine: IUpdate
    {
        private Transform _transform;
        private AtomicVariable<float> _speed;

        private Vector3 _direction;
        
        public void Construct(Transform transform, AtomicVariable<float> speed)
        {
            _transform = transform;
            _speed = speed;
        }
        void IUpdate.Update(float deltaTime)
        {
            _transform.position += _direction * (_speed * deltaTime);
        }
        
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
