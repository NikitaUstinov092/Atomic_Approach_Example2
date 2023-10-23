/*using System;
using Declarative;
using Entities;
using Game.GameEngine.GameResources;
using Game.GameEngine.Mechanics;
using Lessons.Character.Engines;
using Lessons.Gameplay.Interaction;
using Lessons.StateMachines;
using Lessons.StateMachines.StatesComp;
using Lessons.Utils;
using UnityEngine;
using TransformSynchronizer = Lessons.Utils.TransformSynchronizer;

namespace Lessons.Character.Model
{
    [Serializable]
    public sealed class CharacterCore
    {
        [Section]
        public CharacterLife life;

        [Section]
        public CharacterMovement movement;

        [Section]
        public CharacterGathering gathering;

        [Section]
        public CharacterCollision collision;

        [Section]
        public CharacterStates states;
    }

    [Serializable]
    public sealed class CharacterLife
    {
        public AtomicVariable<bool> isAlive;
    }

    [Serializable]
    public sealed class CharacterMovement
    {
        public Transform transform;

        public AtomicVariable<float> MovementSpeed = new(6f);
        public AtomicVariable<float> RotationSpeed = new(10f);
        public MovementDirectionVariable MovementDirection;

        public MoveInDirectionEngine MoveInDirectionEngine;
        public RotateInDirectionEngine RotateInDirectionEngine;

        [Construct]
        public void Construct()
        {
            MoveInDirectionEngine.Construct(transform, MovementSpeed);
            RotateInDirectionEngine.Construct(transform, RotationSpeed);
        }
    }

    [Serializable]
    public sealed class CharacterGathering
    {
        public AtomicVariable<float> duration = new(3);
        public AtomicVariable<float> minDistance = new(1.25f);

        public AtomicProcess<GatherResourceCommand> process;

        [Construct]
        public void Construct(CharacterMovement movement)
        {
            this.process.Condition = cmd =>
            {
                var myPosition = movement.transform.position;
                var resourcePosition = cmd.Position;
                return Vector3.Distance(myPosition, resourcePosition) <= this.minDistance.Value;
            };
                
            this.process.OnStarted += cmd =>
            {
                var myPosition = movement.transform.position;
                var resourcePosition = cmd.Position;
                var direction = (resourcePosition - myPosition).normalized;
                movement.transform.rotation = Quaternion.LookRotation(direction);
            };
        }
    }

    [Serializable]
    public sealed class CharacterCollision
    {
        public CollisionSensor sensor;
        public TransformSynchronizer synchronizer;
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

        [Section]
        public GatherCompositeState gatherState;

        [Construct]
        public void Construct(CharacterModel root)
        {
            root.onStart += () => this.StateMachine.Enter();
        
            StateMachine.Construct(
                (CharacterStateType.Idle, IdleState),
                (CharacterStateType.Run, RunState),
                (CharacterStateType.Dead, DeadState),
                (CharacterStateType.Gathering, gatherState)
            );
        }

        [Construct]
        public void ConstructTransitions(CharacterLife life, CharacterMovement movement, CharacterGathering gathering)
        {
            life.isAlive.ValueChanged += isAlive =>
            {
                var stateType = isAlive ? CharacterStateType.Idle : CharacterStateType.Dead;
                StateMachine.SwitchState(stateType);
            };

            movement.MovementDirection.MovementStarted += () =>
            {
                if (life.isAlive)
                {
                    StateMachine.SwitchState(CharacterStateType.Run);
                }
            };

            movement.MovementDirection.MovementFinished += () =>
            {
                if (life.isAlive && StateMachine.CurrentState == CharacterStateType.Run)
                {
                    StateMachine.SwitchState(CharacterStateType.Idle);
                }
            };

            gathering.process.OnStarted += _ =>
            {
                if (life.isAlive)
                {
                    StateMachine.SwitchState(CharacterStateType.Gathering);
                }
            };
            
            gathering.process.OnStopped += success =>
            {
                if (life.isAlive)
                {
                    StateMachine.SwitchState(CharacterStateType.Idle);
                }
            };
        }
    }
}*/