using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Hero;

namespace GamePlay.StateMachines.States
{
    [Serializable]
    public class RunState : CompositeState
    {
        public MoveState MoveState;
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(MoveState);
        }
        
        [Construct]
        public void ConstructSubStates(HeroModel_View visual, HeroModel_Core.CharacterMovement movement)
        {
            MoveState.Construct(movement.MovementDirection, movement.MoveInDirectionEngine,
                movement.RotateInDirectionEngine);
        }
    }
}
