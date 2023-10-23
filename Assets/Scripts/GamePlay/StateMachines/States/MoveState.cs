using System;
using GamePlay.Components;
using GamePlay.Custom.Engines;
using UnityEngine;

namespace GamePlay.StateMachines.States
{
    [Serializable]
    public sealed class MoveState : IState
    {
        private MovementDirectionVariable _movementDirectionVariable;
        private MoveInDirectionEngine _moveInDirectionEngine;
        private RotateInDirectionEngine _rotateInDirection;
        
        public void Construct(MovementDirectionVariable movementDirection, MoveInDirectionEngine moveInDirectionEngine, RotateInDirectionEngine rotateInDirection)
        {
            _movementDirectionVariable = movementDirection;
            _moveInDirectionEngine = moveInDirectionEngine;
            _rotateInDirection = rotateInDirection;
        }
        
        void IState.Enter()
        {
            _movementDirectionVariable.Subscribe(SetDirection);
            SetDirection(_movementDirectionVariable.Value);
        }

        void IState.Exit()
        {
            _movementDirectionVariable.Unsubscribe(SetDirection);
             SetDirection(Vector3.zero);
        }

        private void SetDirection(Vector3 direction)
        {
            _moveInDirectionEngine.SetDirection(direction);
            _rotateInDirection.SetDirection(direction);
        }
    }
}