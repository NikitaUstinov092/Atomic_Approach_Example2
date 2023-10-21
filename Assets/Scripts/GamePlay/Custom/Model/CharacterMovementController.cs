
using GamePlay.Custom.GameMachine;
using Lessons;
using UnityEngine;

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
