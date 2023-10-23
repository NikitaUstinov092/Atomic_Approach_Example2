using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Zombie;

namespace GamePlay.StateMachines.States
{
   [Serializable]
   public class ChaseState : CompositeState
   {
      private ChaseTarget _chaseTarget = new();
   
      [Construct]
      public void ConstructSelf()
      {
         SetStates(_chaseTarget);
      }
   
      [Construct]
      public void ConstructSubStates(ZombieModel_Core.Chase chase, ZombieModel_Core.TargetChecker targetChecker)
      {
         _chaseTarget.Construct(chase.MoveTransform,targetChecker.Target,chase.MinSpeed.Value,chase.MaxSpeed.Value);
      }
   }
}

