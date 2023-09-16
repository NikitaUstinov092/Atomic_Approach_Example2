using System;
using System.Atomic.Implementations;
using UnityEngine;


    [Serializable]
    public sealed class MoveInDirectionEngine
    {
        private Transform _transform;
        private AtomicVariable<float> _speed;

        private Vector3 _direction;
        
        public void Construct(Transform transform, AtomicVariable<float> speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void UpdatePosition()
        {
            _transform.position += _direction * (_speed.Value * Time.deltaTime);
            Debug.Log(_direction);
        }
        
        /*void IUpdateListener.Update()
        {
            
        }*/

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
