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
using UnityEngine.Serialization;
using UpdateMechanics;
using Object = UnityEngine.Object;

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
            public Rotate RotateComp = new();
            
            [Section]
            [SerializeField]
            public Shoot ShootComp = new();
            
            [Section]
            [SerializeField]
            public Ammo AmmoComp = new();
            
            [Section]
            [SerializeField]
            public TargetEntitySection EntityTarget = new();
            
            [Section]
            [SerializeField]
            public EntityContainer EntityStorageComp = new();
        
            
            [Serializable]
            public sealed class CharacterMovement
            {
                    public Transform Transform;
            
                    public AtomicVariable<float> movementSpeed = new(6f);
                    public AtomicVariable<float> rotationSpeed = new(10f);
                    public MovementDirectionVariable movementDirection;
            
                    public MoveInDirectionEngine moveInDirectionEngine;
                    public RotateInDirectionEngine rotateInDirectionEngine;
            
                    [Construct]
                    public void Construct()
                    {
                        moveInDirectionEngine.Construct(Transform, movementSpeed);
                        rotateInDirectionEngine.Construct(Transform, rotationSpeed);
                    }
            }
            
            [Serializable]
            public sealed class Rotate
            {
                public AtomicVariable<Vector3> RotationDirection;
            
                public AtomicVariable<float> RotationSpeed;
           
                [SerializeField]
                private Camera _playerCamera;
                
                [SerializeField]
                private Transform _playerTransform;
                
                private readonly FixedUpdateMechanics _fixedUpdate = new();
                private RotationEngine _rotationMotor = new();

                [Construct]
                public void Construct(LifeSection lifeSection)
                {
                    var isDeath = lifeSection.IsDead;

                    _rotationMotor.Construct(_playerTransform, _playerCamera, RotationSpeed.Value);
                
                    _fixedUpdate.Construct(_ =>
                    {
                        if(isDeath.Value 
                           || RotationDirection.Value == Vector3.zero)
                            return;
                    
                        var cursorScreenPos = RotationDirection.Value;
                        _rotationMotor.UpdateRotation(cursorScreenPos);
                    });
                }
            }

            [Serializable]
            public sealed class Shoot
            {
                public AtomicEvent OnGetPressedFire = new();
                public AtomicEvent OnBulletCreated = new();
                
                public AtomicVariable<BulletConfig> BulletConfig = new();
                public AtomicVariable<float> CoolDown = new();
                
                [SerializeField]
                private ShootEngine _shootEngine;
                
                [SerializeField]
                private Transform _spawnPointShoot;
                
                private readonly FixedUpdateMechanics _fixedUpdate = new();
                
                private bool _canShoot = true;
                private bool _coolDown;
                private float _timer;
                
                [Construct]
                public void Construct(Ammo ammo, LifeSection lifeSection)
                {
                    _shootEngine.Construct(BulletConfig.Value, _spawnPointShoot);
                
                    var isDead = lifeSection.IsDead;
                    var ammoCount = ammo.AmmoCount;
                    
                    isDead.Subscribe((data) => _canShoot = !data);
                    ammoCount.Subscribe((count) => _canShoot = (count > 0));

                    if (ammoCount.Value <= 0)
                        _canShoot = false;
                
                    OnGetPressedFire.Subscribe(() =>
                    {
                        if(!_canShoot || _coolDown)
                            return;
                    
                        _coolDown = true;
                        _shootEngine.CreateBullet();
                        OnBulletCreated?.Invoke();
                    });
                
                    _fixedUpdate.Construct(deltaTime => 
                    {
                        if(!_canShoot || !_coolDown)
                            return;
                        
                        _timer += deltaTime;

                        if (_timer <= CoolDown.Value || !_canShoot) 
                            return;
                        
                        _timer = 0;
                        _coolDown = false;
                    }); 
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
                    var shoot = shootComp.OnBulletCreated;
                    
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
        public StateMachine<CharacterStateType> stateMachine;

        [FormerlySerializedAs("idleAutoState")] [FormerlySerializedAs("shootAutoState")] [Section]
        public IdleState idleState;

        [Section]
        public RunState runState;

        [Section]
        public DeadState deadState;
        

        [Construct]
        public void Construct(HeroModel root)
        {
            root.onStart += () => stateMachine.Enter();
        
            stateMachine.Construct(
                (CharacterStateType.Idle, idleState),
                (CharacterStateType.Run, runState),
                (CharacterStateType.Dead, deadState)
            );
        }

        [Construct]
        public void ConstructTransitions(LifeSection life, CharacterMovement movement)
        {
            var isDead = life.DeathEvent;
            isDead.Subscribe(() => stateMachine.SwitchState(CharacterStateType.Dead));
            
            
            movement.movementDirection.MovementStarted.Subscribe(()=>
            {
                if (!life.IsDead.Value)
                {
                    stateMachine.SwitchState(CharacterStateType.Run);
                }
            }); 

            movement.movementDirection.MovementFinished.Subscribe(() =>
            {
                if (!life.IsDead.Value && stateMachine.CurrentState == CharacterStateType.Run)
                {
                    stateMachine.SwitchState(CharacterStateType.Idle);
                }
            });
        }
    }
    
    
    [Serializable]
    public sealed class EntityContainer
    {
        public Entity.Entity Entity;

        [Construct]
        public void Construct(LifeSection lifeSection)
        {
            lifeSection.DeathEvent.Subscribe(() =>
            {
                Object.Destroy(Entity);
            });
        }
        
    }
        }
    }
