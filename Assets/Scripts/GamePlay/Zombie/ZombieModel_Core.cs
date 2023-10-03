using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Components.Interfaces;
using GamePlay.Custom.Sections;
using GamePlay.Hero;
using Lessons.StateMachines.States;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMechanics;
using Random = UnityEngine.Random;

    namespace GamePlay.Zombie
    {
        [Serializable]
        public sealed class ZombieModel_Core
        {
            [Section]
            [SerializeField]
            public LifeSection lifeSection = new();

            [Section]
            [SerializeField] 
            public Chase ZombieChase = new();
            
            [Section]
            [SerializeField] 
            public TargetChecker DisctanceChecker = new();
            
            [Section]
            [SerializeField] 
            public TargetEntitySection TargetSection = new();
        
            [Section]
            [SerializeField] 
            public Attack AttackHero = new();
            
            [Section] 
            [SerializeField] 
            public ZombieStates ZombieState = new();
        
            [Serializable]
            public sealed class Chase
            {
                public AtomicVariable<Transform> MoveTransform;
                public AtomicVariable<float> MinSpeed = new();
                public AtomicVariable<float> MaxSpeed = new();
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
                public void Construct(TargetEntitySection targetEntitySection)
                {
                    targetEntitySection.TargetEntity.onChanged += (entity) => Target.Value = entity;
                    
                    _fixedUpdate.Construct(_ =>
                    {
                        if (Target.Value == null)
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
                            targetChecker.Target.Value.TryGet(out ITakeDamageable damage))
                            damage.TakeDamage(Damage.Value);
                        
                        _timer = 0f;
                    });
                }
            }
            
            [Serializable]
            public sealed class ZombieStates
            {
                public StateMachine<EnemyStatesType> stateMachine;

                [Section]
                public ChaseState chase;

                [Section]
                public AttackState runState;

                /*[Section]
                public DeadState deadState;*/
        

                [Construct]
                public void Construct(ZombieModel root)
                {
                    root.onStart += () => stateMachine.Enter();
        
                    stateMachine.Construct(
                        (EnemyStatesType.Chase, chase)/*,
                        (EnemyStatesType.Attack, runState),
                        (EnemyStatesType.Death, deadState)*/
                    );
                }

                [Construct]
                public void ConstructTransitions(LifeSection life, TargetChecker targetChecker)
                {
                    var isDead = life.DeathEvent;
                    isDead.Subscribe(() => stateMachine.SwitchState(EnemyStatesType.Death));
                    
                    targetChecker.ClosedTarget.Subscribe((value)=>
                    {
                        if (!life.IsDead.Value)
                        {
                            stateMachine.SwitchState(value ? EnemyStatesType.Attack : EnemyStatesType.Chase);
                        }
                    }); 
                    
                }
            }

        }
    }
