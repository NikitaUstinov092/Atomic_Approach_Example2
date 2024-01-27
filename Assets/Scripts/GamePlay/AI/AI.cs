using System.Atomic.Implementations;
using System.Declarative.Scripts;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Engines;
using GamePlay.Custom.Model;
using GamePlay.Hero;
using GamePlay.StateMachines.States;
using UnityEngine;
using Zenject;


namespace GamePlay.AI
{
   public class AI : DeclarativeModel
   {
      [Inject]
      public EnemyService EnemyService;
      
      public AtomicVariable<bool> AutoMode = new();
      public AtomicVariable<HeroModel> Hero = new();
      public AtomicVariable<Entity.Entity> TargetEnemy = new();
      public AtomicVariable<float> RotationSpeed = new();
      public AtomicVariable<float> AngleThreshold = new();
      public AtomicVariable<Vector3> EnemyDirection = new();

      private RotateInDirectionMechanics _rotateInDirectionMechanics = new();
      private EnemySelectMechanics _enemySelectMechanics = new();
      private AutoAttackMechanics _autoAttackMechanics = new();

      [Construct]
      public void Construct()
      {
         var core = Hero.Value.Core;
         
         core.StatesComp.OnStateChanged.Subscribe((state) =>
         {
            if (state != CharacterStateType.Idle)
            {
               AutoMode.Value = false;
               _rotateInDirectionMechanics.SetDirection(Vector3.zero);
            }
         });
         
         _rotateInDirectionMechanics.Construct(core.CharacterMoveComp.Transform,EnemyDirection, RotationSpeed);
         _enemySelectMechanics.Construct(EnemyService, TargetEnemy, Hero, AutoMode);
         _autoAttackMechanics.Construct(AutoMode, core.CharacterMoveComp.Transform, core.ShootComp.FireRequest, TargetEnemy, AngleThreshold, EnemyDirection);
      }
   }
}