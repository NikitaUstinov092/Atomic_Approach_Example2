using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Engines;
using GamePlay.Custom.Model;
using GamePlay.Hero;
using GamePlay.StateMachines.States;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMechanics;

namespace GamePlay.AI
{
   public class AI : DeclarativeModel
   {
      public HeroModel Character;
      public bool Activate;

      [FormerlySerializedAs("_closestEntitySearcher")] [SerializeField] 
      private ClosestEnemySearcher closestEnemySearcher;
   
      private RotateToEnemyMechanics _rotateToEnemyMechanics = new();
      private ShootEnemyMechanics _shootEnemyMechanics = new();

      private readonly FixedUpdateMechanics _fixedUpdate = new();
  
      public AtomicVariable<Entity.Entity> _entityEnemy; 
   
      [Construct]
      public void Construct()
      {
         var core = Character.Core;
         var moveComp = Character.Core.CharacterMoveComp;
      
         core.StatesComp.OnStateChanged.Subscribe((value) => { Activate = value == CharacterStateType.Idle; });
         closestEnemySearcher.OnClosestEntityChanged.Subscribe((entity) => _entityEnemy.Value = entity);
      
         _rotateToEnemyMechanics.Construct(moveComp.Transform, moveComp.RotateInDirectionEngine);
        // _shootEnemyMechanics.Construct(core.ShootComp.ShootMechanics);
       
      
         _fixedUpdate.Construct((_) =>
         {
            if(Activate)
               closestEnemySearcher.Update();
            
            if(_entityEnemy.Value == null || !Activate)
               return;
         
            _rotateToEnemyMechanics.SetDirection(_entityEnemy.Value.transform.position);
            _shootEnemyMechanics.Update();
         
         });
      }
   }

   [Serializable]
   public sealed class RotateToEnemyMechanics
   {
      private RotateInDirectionEngine _rotateInDirection;
      private Transform _heroTransform;
      private Vector3 _positionEnemy;
   
      public void Construct(Transform heroTransform,
         RotateInDirectionEngine rotateInDirection)
      {
         _rotateInDirection = rotateInDirection;
         _heroTransform = heroTransform;
      }
   
      public void SetDirection(Vector3 entityPosition)
      {
         _positionEnemy =  entityPosition - _heroTransform.position;
         _rotateInDirection.SetDirection(_positionEnemy);
      }
   }

   [Serializable]
   public sealed class ShootEnemyMechanics
   {
      private ShootMechanics _shootMechanics;
    
      public void Construct(ShootMechanics shootMechanics)
      {
         _shootMechanics = shootMechanics;
      }
      public void Update()
      {
        // _shootMechanics.FireRequest.Invoke();
      }
   }
}