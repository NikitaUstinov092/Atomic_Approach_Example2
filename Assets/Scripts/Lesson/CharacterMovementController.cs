using System.Declarative.Scripts;
using GamePlay.Custom.GameMachine;
using Lessons.Character.Components;
using UnityEngine;

namespace Lessons.Character.Controllers
{
    public sealed class CharacterMovementController : MonoBehaviour, IStartListener, IDisable
    {
        [SerializeField]
        private NewInput movementInput;
         
        [SerializeField]
        private Entity.Entity character;

        private MoveInDirectionComponent _moveInDirection;
        
        private void OnMovementDirectionChanged(Vector3 direction)
        {
            _moveInDirection.MoveInDirection(direction);
        }

        void IStartListener.StartGame()
        {
            _moveInDirection = character.Get<MoveInDirectionComponent>();
            movementInput.MovementDirectionChanged += OnMovementDirectionChanged;
        }

        void IDisable.OnDisable()
        {
            movementInput.MovementDirectionChanged -= OnMovementDirectionChanged;
        }
    }
}