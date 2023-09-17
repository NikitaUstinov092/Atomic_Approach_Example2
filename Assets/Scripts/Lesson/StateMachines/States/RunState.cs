using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Hero;
using Lessons.StateMachines.States;


[Serializable]
    public sealed class RunState : CompositeState
    {
        public MoveState moveState;
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(moveState);
        }
        
        [Construct]
        public void ConstructSubStates(HeroModel_View visual, HeroModel_Core.Move movement)
        {
            moveState.Construct(movement.MovementDirection, movement.MoveInDirectionEngine);
                /*movement.rotateInDirectionEngine*/;
        }
    }
