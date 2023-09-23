using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Hero;
using Lesson.StateMachines.States;

namespace Lessons.StateMachines.States
{
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
        public void ConstructSubStates(HeroModel_View visual, HeroModel_Core core)
        {
            ShootState.Construct(core.entityTarget, core.ShootComp);
            RotateToEnemyState.Construct(core.entityTarget, core.MoveComp.MoveTransform);
        }
    }
}