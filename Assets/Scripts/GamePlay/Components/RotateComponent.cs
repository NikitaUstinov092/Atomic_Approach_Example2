using System.Atomic.Interfaces;
using GamePlay.Components.Interfaces;
using UnityEngine;

namespace GamePlay.Components
{
    public class RotateComponent : IRotatable
    {
        private readonly IAtomicVariable<Vector3> _direction;

        public RotateComponent(IAtomicVariable<Vector3> direction)
        {
            _direction = direction;
        }
    
        void IRotatable.RotateInDirection(Vector3 direction)
        {
            _direction.Value = direction;
        }
    }
}
