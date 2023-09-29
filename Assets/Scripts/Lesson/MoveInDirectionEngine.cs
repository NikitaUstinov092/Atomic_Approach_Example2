using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;


    [Serializable]
    public sealed class MoveInDirectionEngine: IFixedUpdate
    {
        private Transform _transform;
        private AtomicVariable<float> _speed;

        private Vector3 _direction;
        
        public void Construct(Transform transform, AtomicVariable<float> speed)
        {
            _transform = transform;
            _speed = speed;
        }
        
        public void SetDirection(Vector3 direction)
        {
            var moveVector = _transform.forward * direction.z + _transform.right * direction.x;
            _direction = moveVector.normalized;
        }

        void IFixedUpdate.FixedUpdate(float deltaTime)
        {
            _transform.position += _direction * (_speed.Value * deltaTime);
        }
    }
