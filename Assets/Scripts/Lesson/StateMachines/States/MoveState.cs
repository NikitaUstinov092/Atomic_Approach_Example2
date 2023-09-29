using System;
using Lesson;
using Lessons.Character.Engines;
using UnityEngine;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class MoveState : IState
    {
        private MovementDirectionVariable _movementDirection;
        private MoveInDirectionEngine _moveInDirectionEngine;
        
        private RotateInDirectionEngine _rotateInDirection;
        

        public void Construct(MovementDirectionVariable movementDirection, MoveInDirectionEngine moveInDirectionEngine, RotateInDirectionEngine rotateInDirection)
        {
            _movementDirection = movementDirection;
            _moveInDirectionEngine = moveInDirectionEngine;
            _rotateInDirection = rotateInDirection;
        }
        
        void IState.Enter()
        {
            _movementDirection.Subscribe(SetDirection);
            SetDirection(_movementDirection.Value);
        }

        void IState.Exit()
        {
            _movementDirection.Unsubscribe(SetDirection);
             SetDirection(Vector3.zero);
        }

        private void SetDirection(Vector3 direction)
        {
            _moveInDirectionEngine.SetDirection(direction);
            _rotateInDirection.SetDirection(direction);
        }
    }
}