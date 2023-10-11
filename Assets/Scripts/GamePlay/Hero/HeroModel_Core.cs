using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Engines;
using GamePlay.Custom.ScriptableObjects;
using GamePlay.Custom.Sections;
using Lesson;
using Lessons.Character.Engines;
using Lessons.StateMachines.States;
using UnityEngine;
using UpdateMechanics;

    namespace GamePlay.Hero
    {
        [Serializable]
        public sealed class HeroModel_Core
        {
            [Section]
            [SerializeField]
            public LifeSection LifeSectionComp = new();
            
            [Section]
            [SerializeField]
            public CharacterMovement CharacterMoveComp = new();
            
            [Section]
            [SerializeField]
            public CharacterStates States = new();
            
            [Section]
            [SerializeField]
            public Shoot ShootComp = new();
            
            [Section]
            [SerializeField]
            public Ammo AmmoComp = new();
            
            [Section]
            [SerializeField]
            public TargetEntitySection EntityTarget = new();
            
            
            [Serializable]
            public sealed class CharacterMovement
            {
                    public Transform Transform;
            
                    public AtomicVariable<float> MovementSpeed = new(6f);
                    public AtomicVariable<float> RotationSpeed = new(10f);
                    
                    public MovementDirectionVariable MovementDirection = new();
                    public MoveInDirectionEngine MoveInDirectionEngine = new();
                    public RotateInDirectionEngine RotateInDirectionEngine = new();
            
                    [Construct]
                    public void Construct()
                    {
                        MoveInDirectionEngine.Construct(Transform, MovementSpeed);
                        RotateInDirectionEngine.Construct(Transform, RotationSpeed);
                    }
            }

            [Serializable]
            public sealed class Shoot
            {
                public AtomicVariable<BulletConfig> BulletConfig = new();
                public AtomicVariable<float> CoolDown = new();
                
                public AtomicVariable<Transform> SpawnPointShoot = new();
                public ShootController ShootController;
                public ShootEngine ShootEngine = new();
                
                [Construct]
                public void Construct(Ammo ammo, LifeSection life)
                {
                    ShootEngine.Construct(BulletConfig.Value, SpawnPointShoot.Value);
                    ShootController.Construct(this, ammo.AmmoCount, life.IsDead);
                }
            }
            
        
            [Serializable]
            public sealed class Ammo
            {
                public AtomicVariable<int> AmmoCount = new();
                public AtomicVariable<int> MaxAmmo = new ();
                public AtomicVariable<float> ReloadTime = new ();
                
                private readonly FixedUpdateMechanics _fixedUpdate = new();
                
                private bool _reloadRequired;
                private bool _canReload = true;
                private float _timer;
                
                [Construct]
                public void Construct(Shoot shootComp, LifeSection lifeSectionComp)
                {
                    var isDead = lifeSectionComp.IsDead;
                    var shoot = shootComp.ShootController.OnBulletCreated;
                    
                    isDead.Subscribe((data) => _canReload = !data);
                    
                    AmmoCount.Subscribe(count => _reloadRequired = count < MaxAmmo.Value);

                    if (AmmoCount.Value < MaxAmmo.Value)
                        _reloadRequired = true;
                    
                    shoot.Subscribe(() =>
                    {
                        if(AmmoCount.Value <= 0 || !_canReload)
                            return;
                        
                        AmmoCount.Value--;
                    });
                    
                    _fixedUpdate.Construct(deltaTime => 
                    {
                        if(!_reloadRequired || !_canReload)
                            return;
                    
                        _timer += deltaTime;

                        if (_timer <= ReloadTime.Value || !_canReload) 
                            return;
                        
                        AmmoCount.Value++;
                        
                        _timer = 0;
                    }); 
                }
            }
    [Serializable]
    public sealed class CharacterStates
    {
        public StateMachine<CharacterStateType> StateMachine;

        [Section]
        public IdleState IdleState;

        [Section]
        public RunState RunState;

        [Section]
        public DeadState DeadState;
        

        [Construct]
        public void Construct(HeroModel root)
        {
            root.onStart += () => StateMachine.Enter();
        
            StateMachine.Construct(
                (CharacterStateType.Idle, IdleState),
                (CharacterStateType.Run, RunState),
                (CharacterStateType.Dead, DeadState)
            );
        }

        [Construct]
        public void ConstructTransitions(LifeSection life, CharacterMovement movement)
        {
            var isDead = life.DeathEvent;
            isDead.Subscribe(() => StateMachine.SwitchState(CharacterStateType.Dead));
            
            
            movement.MovementDirection.MovementStarted.Subscribe(()=>
            {
                if (!life.IsDead.Value)
                {
                    StateMachine.SwitchState(CharacterStateType.Run);
                }
            }); 

            movement.MovementDirection.MovementFinished.Subscribe(() =>
            {
                if (!life.IsDead.Value && StateMachine.CurrentState == CharacterStateType.Run)
                {
                    StateMachine.SwitchState(CharacterStateType.Idle);
                }
            });
        }
        
    }
        }
    }
