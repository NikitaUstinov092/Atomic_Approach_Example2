using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Components.Interfaces;
using GamePlay.Custom.Sections;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMechanics;
using Random = UnityEngine.Random;

    namespace GamePlay.Zombie
    {
        [Serializable]
        public sealed class ZombieModel_Core
        {
            [FormerlySerializedAs("Life")]
            [Section]
            [SerializeField]
            public LifeSection lifeSection = new();

            [Section]
            [SerializeField] 
            public Chase ZombieChase = new();
            
            [FormerlySerializedAs("TargetDistance")]
            [Section]
            [SerializeField] 
            public TargetChecker target = new();
        
            [Section]
            [SerializeField] 
            public Attack AttackHero = new();
        
            [Serializable]
            public sealed class Chase
            {
                [SerializeField]
                public Transform MoveTransform;
                
                public AtomicVariable<float> MinSpeed = new();
                public AtomicVariable<float> MaxSpeed = new();
                public AtomicVariable<bool> IsChasing = new();
            
                private float _speed;
                private readonly FixedUpdateMechanics _fixedUpdate = new();

                [Construct]
                public void Construct(LifeSection lifeSection, TargetChecker targetChecker)
                {
                    var isDeath = lifeSection.IsDead;
                    var closedToTarget = targetChecker.ClosedTarget;
                    
                    _speed = Random.Range(MinSpeed.Value, MaxSpeed.Value);

                    _fixedUpdate.Construct(deltaTime =>
                    {
                        if (isDeath.Value || closedToTarget.Value || targetChecker.Target.Value == null)
                        {
                            IsChasing.Value = false;
                            return;
                        }
                        var targetPosition = targetChecker.Target.Value.transform.position;
                    
                        MoveTransform.position = Vector3.MoveTowards( MoveTransform.position, targetPosition, _speed * deltaTime);
                        MoveTransform.LookAt(targetPosition);
                        IsChasing.Value = true;
                    });
                }
            }
        
            [Serializable]
            public sealed class TargetChecker
            {
                [SerializeField]
                public Transform MoveTransform;
                
                public AtomicVariable<Entity.Entity> Target = new();
                
                public AtomicVariable<float> DistanceTarget = new();
                
                public AtomicVariable<bool> ClosedTarget = new();
            
                private readonly FixedUpdateMechanics _fixedUpdate = new();

                [Construct]
                public void Construct()
                {
                    _fixedUpdate.Construct(_ =>
                    {
                        if(Target.Value == null)
                            return;
                        var distance = Vector3.Distance(MoveTransform.position, Target.Value.transform.position);
                        ClosedTarget.Value = distance < DistanceTarget.Value;
                    });
                }
            }
        
            [Serializable]
            public sealed class Attack
            {
                public AtomicVariable<int> Damage = new();
                public AtomicVariable<float> AttackDelay = new();
                public AtomicVariable<bool> StopAttack = new();
                
                private readonly LateUpdateMechanics _lateUpdate = new();
                private float _timer;
            
                [Construct]
                public void Construct(TargetChecker targetChecker, LifeSection lifeSection)
                {
                    var target =  targetChecker.Target;
                    target.Subscribe((entity) => StopAttack.Value = entity == null);
                    lifeSection.IsDead.Subscribe((dead) => StopAttack.Value = dead);
                    
                    _lateUpdate.Construct(deltaTime=>
                    {
                        if(StopAttack.Value)
                            return;
                       
                        if (!targetChecker.ClosedTarget.Value)
                            return;
                      
                        _timer += deltaTime;
                        
                        if (!(_timer >= AttackDelay.Value || lifeSection.IsDead.Value)) 
                            return;
                        
                        if (targetChecker.Target.Value != null && 
                            targetChecker.Target.Value.TryGet(out ITakeDamagable damage))
                            damage.TakeDamage(Damage.Value);
                        
                        _timer = 0f;
                    });
                }
            }
        }
    }
