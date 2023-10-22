using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Engines;
using GamePlay.Hero;
using Lessons.Character.Engines;
using Lessons.StateMachines.States;
using UnityEngine;
using UpdateMechanics;

public class AI : DeclarativeModel
{
   public HeroModel Character;
   public bool Activate;
   
   private RotateToEnemyMechanics _rotateToEnemyMechanics = new();
   private ShootEnemyMechanics _shootEnemyMechanics = new();

   private readonly FixedUpdateMechanics _fixedUpdate = new();
  
   private AtomicVariable<Entity.Entity> _entityEnemy; //Переделать, убрать из core
   
   [Construct]
   public void Construct()
   {
      var core = Character.Core;
      var moveComp = Character.Core.CharacterMoveComp;
      
      core.States.OnStateChanged.Subscribe((value) => { Activate = value == CharacterStateType.Idle; });
      
      _rotateToEnemyMechanics.Construct(moveComp.Transform, moveComp.RotateInDirectionEngine);
      _shootEnemyMechanics.Construct(core.ShootComp.ShootController);

      _entityEnemy = core.EntityTarget.TargetEntity;
      
      _fixedUpdate.Construct((_) =>
      {
         if(!Activate || _entityEnemy.Value == null)
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
   private ShootController _shootController;
    
   public void Construct(ShootController shootController)
   {
      _shootController = shootController;
   }
   public void Update()
   {
      _shootController.FireRequest.Invoke();
   }
}


