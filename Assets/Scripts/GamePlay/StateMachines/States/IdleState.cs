using System;

namespace GamePlay.StateMachines.States
{
    [Serializable]
    public sealed class IdleState : CompositeState
    {
        /*public AutoShootState ShootState = new();
        public RotateToEnemyState RotateToEnemyState = new();
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(ShootState, RotateToEnemyState);
        }
        
        [Construct]
        public void ConstructSubStates(HeroModel_Core core, HeroModel_View view)
        {
            ShootState.Construct(core.EntityTarget, core.ShootComp.ShootMechanics, view.AnimMachine);
            RotateToEnemyState.Construct(core.EntityTarget, core.CharacterMoveComp.Transform, core.CharacterMoveComp.RotateInDirectionMechanics);
        }*/
        
    }
}
