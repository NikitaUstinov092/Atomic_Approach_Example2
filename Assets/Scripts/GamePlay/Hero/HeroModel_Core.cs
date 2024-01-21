using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Components;
using GamePlay.Custom.Engines;
using GamePlay.Custom.ScriptableObjects;
using GamePlay.Custom.Sections;
using GamePlay.StateMachines;
using GamePlay.StateMachines.States;
using UnityEngine;

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
        public CharacterStates StatesComp = new();

        [Section] 
        [SerializeField] 
        public Shoot ShootComp = new();

        [Section] 
        [SerializeField] 
        public Ammo AmmoComp = new();

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
            public AtomicVariable<float> CoolDownDelay = new();
            public AtomicVariable<bool> WeaponCooled = true;
            public AtomicVariable<bool> CanShoot = new();
            public AtomicVariable<Transform> SpawnPointShoot = new();
          
            public AtomicEvent FireRequest = new();
            public AtomicEvent ShootApplied = new();
            public AtomicEvent OnShoot = new();
            
            private readonly ShootMechanics _shootMechanics = new();
            private readonly ShootEngine _shootEngine = new();
            private readonly CoolDownMechanics _coolDownMechanics = new();

            [Construct]
            public void Construct(Ammo ammo, LifeSection life)
            {
                WeaponCooled.Value = true;
                CanShoot.Value = true;
                
                _shootEngine.Construct(BulletConfig.Value, SpawnPointShoot.Value);
                _shootMechanics.Construct(_shootEngine, FireRequest, ShootApplied, OnShoot, ammo.AmmoCount, life.IsDead, WeaponCooled, CanShoot);
                _coolDownMechanics.Construct(CoolDownDelay, WeaponCooled, OnShoot, null);
            }
        }


        [Serializable]
        public sealed class Ammo
        {
            public AtomicEvent OnReloadComplete = new(); 
            public AtomicEvent ReloadRequest= new(); 
            
            public AtomicVariable<int> AmmoCount = new();
            public AtomicVariable<int> MaxAmmo = new();
            public AtomicVariable<float> ReloadTime = new();
            
            private AtomicVariable<bool> _reloading = new();
           
            private readonly ReloadMechanics _reloadMechanics = new();
            private readonly CoolDownMechanics _coolDownMechanics = new();

            [Construct]
            public void Construct(Shoot shootComp, LifeSection lifeSectionComp)
            {
                _reloading.Value = true;
                
                var isDead = lifeSectionComp.IsDead;
                var shoot = shootComp.OnShoot;
                
                isDead.Subscribe((data) => _reloading.Value = data);

                shoot.Subscribe(() =>
                {
                    if (AmmoCount.Value > 0)
                        AmmoCount.Value--;
                });
               
                _coolDownMechanics.Construct(ReloadTime, _reloading, ReloadRequest, OnReloadComplete);
                _reloadMechanics.Construct(AmmoCount, MaxAmmo, ReloadRequest, OnReloadComplete);
            }
        }

        [Serializable]
        public sealed class CharacterStates
        {
            public StateMachine<CharacterStateType> StateMachine;
            public AtomicEvent<CharacterStateType> OnStateChanged;

            [Section] 
            public IdleState IdleState;

            [Section] 
            public RunState RunState;

            [Section] 
            public DeadState DeadState;
            
            [Construct]
            public void Construct(HeroModel root)
            {
                root.onStart += () =>
                {
                    StateMachine.Enter();
                    OnStateChanged?.Invoke(CharacterStateType.Idle);
                };

                StateMachine.Construct(
                    (CharacterStateType.Idle, IdleState),
                    (CharacterStateType.Move, RunState),
                    (CharacterStateType.Death, DeadState)
                );
            }

            [Construct]
            public void ConstructTransitions(LifeSection life, CharacterMovement movement)
            {
                var isDead = life.DeathEvent;
                isDead.Subscribe(() =>
                {
                    const CharacterStateType deathState = CharacterStateType.Death;
                    StateMachine.SwitchState(deathState);
                    OnStateChanged?.Invoke(deathState);
                });
                
                movement.MovementDirection.MovementStarted.Subscribe(() =>
                {
                    if (life.IsDead.Value)
                        return;
                    
                    const CharacterStateType moveState = CharacterStateType.Move;
                    StateMachine.SwitchState(moveState);
                    OnStateChanged?.Invoke(moveState);
                });

                movement.MovementDirection.MovementFinished.Subscribe(() =>
                {
                    if (life.IsDead.Value || StateMachine.CurrentState != CharacterStateType.Move) 
                        return;
                    
                    const CharacterStateType idleState = CharacterStateType.Idle;
                    StateMachine.SwitchState(idleState);
                    OnStateChanged?.Invoke(idleState);
                });
            }
        }
    }
}