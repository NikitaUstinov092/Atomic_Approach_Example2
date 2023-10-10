using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Hero;
using Lesson.StateMachines.States;
using Lessons.StateMachines.States;


    [Serializable]
    public sealed class IdleState : CompositeState
    {
        public AutoShootState ShootState = new();
        public RotateToEnemyState RotateToEnemyState = new();
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(ShootState, RotateToEnemyState);
        }
        
        [Construct]
        public void ConstructSubStates(HeroModel_Core core)
        {
            ShootState.Construct(core.EntityTarget, core.ShootComp.ShootController);
            RotateToEnemyState.Construct(core.EntityTarget, core.CharacterMoveComp.Transform, core.CharacterMoveComp.RotateInDirectionEngine);
        }
        
    }
