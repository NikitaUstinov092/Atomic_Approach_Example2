using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using GamePlay.Custom.Input;
using UnityEngine;

namespace GamePlay.Custom.Model
{
    public sealed class CharacterMovementController : MonoBehaviour, IInitListener, IDisableListener
    {
        [SerializeField] private MovementInput movementInput;

        [SerializeField] private Entity.Entity character;

        private MoveInDirectionComponent _moveInDirection;
        
        void IInitListener.OnInit()
        {
            _moveInDirection = character.Get<MoveInDirectionComponent>();
            movementInput.MovementDirectionChanged += OnMovementDirectionChanged;
        }
        void IDisableListener.Disable()
        {
            movementInput.MovementDirectionChanged -= OnMovementDirectionChanged;
        }
        private void OnMovementDirectionChanged(Vector3 direction)
        {
            _moveInDirection.MoveInDirection(direction);
        }
    }
}
