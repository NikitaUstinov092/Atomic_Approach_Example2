using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Hero;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class IdleState : CompositeState
    {
        public AutoShootState ShootState = new();
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(ShootState);
        }
        
        [Construct]
        public void ConstructSubStates(HeroModel_View visual, HeroModel_Core core)
        {
            core.ShootComp.OnGetPressedFire.Invoke();
            ShootState.Construct(core.EnemyTarget, core.RotateComp,core.ShootComp);
        }

    }
}