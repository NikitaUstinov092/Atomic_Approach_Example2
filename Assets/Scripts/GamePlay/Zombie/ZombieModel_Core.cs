using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Components;
using GamePlay.Custom.Engines;
using GamePlay.Custom.Sections;
using GamePlay.StateMachines;
using GamePlay.StateMachines.States;
using UnityEngine;
using UpdateMechanics;


    namespace GamePlay.Zombie
    {
        [Serializable]
        public sealed class ZombieModel_Core
        {
            [Section]
            [SerializeField]
            public LifeSection LifeSection = new();

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
                public AttackEngine AttackEngine = new();
                
                [Construct]
                public void Construct(TargetChecker targetChecker)
                {
                    var target =  targetChecker.Target;
                    AttackEngine.Construct(AttackDelay, Damage, target);
                }
            }
            
            [Serializable]
            public sealed class ZombieStates
            {
                public StateMachine<EnemyStatesType> StateMachine;

                [Section]
                public ChaseState Сhase;

                [Section]
                public AttackState StateAttack;

                [Section]
                public DeadState DeadState;
                
                [Construct]
                public void Construct(ZombieModel root)
                {
                    root.onStart += () => StateMachine.Enter();
        
                    StateMachine.Construct(
                        (EnemyStatesType.Chase, Сhase),
                        (EnemyStatesType.Attack, StateAttack),
                        (EnemyStatesType.Death, DeadState)
                    );
                }

                [Construct]
                public void ConstructTransitions(LifeSection life, TargetChecker targetChecker)
                {
                    var isDead = life.DeathEvent;
                    isDead.Subscribe(() => StateMachine.SwitchState(EnemyStatesType.Death));
                    
                    targetChecker.ClosedTarget.Subscribe((value)=>
                    {
                        if (!life.IsDead.Value)
                        {
                            StateMachine.SwitchState(value ? EnemyStatesType.Attack : EnemyStatesType.Chase);
                        }
                    });

                    targetChecker.Target.Subscribe(entity =>
                    {
                        if (entity.TryGet(out DeathEventComponent deathEventComponent))
                        {
                            deathEventComponent.GetDeathEvent().Subscribe(() => StateMachine.SwitchState(EnemyStatesType.Idle));
                        }
                    });
                }
            }
        }
    }
