using System;
using System.Declarative.Scripts.Attributes;
using GamePlay.Zombie;
using Lessons.StateMachines.States;

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
   public void ConstructSubStates(ZombieModel_Core.TargetChecker targetChecker, ZombieModel_Core.Chase chase)
   {
      _chaseTarget.Construct(chase, targetChecker);
   }
}

