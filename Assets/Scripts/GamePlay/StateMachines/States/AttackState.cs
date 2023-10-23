using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Zombie;

namespace GamePlay.StateMachines.States
{
    [Serializable]
    public class AttackState : CompositeState
    {
        public KickState KickState = new();
        
        [Construct]
        public void ConstructSelf()
        {
            SetStates(KickState);
        }
        
        [Construct]
        public void ConstructSubStates(ZombieModel_Core.Attack attack)
        {
            KickState.Construct(attack.AttackEngine);
        }
    }
}
