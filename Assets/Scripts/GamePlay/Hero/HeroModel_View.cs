using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using Lessons.Character.Animations;
using Lessons.StateMachines.States;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMechanics;
using Vector3 = UnityEngine.Vector3;

    namespace GamePlay.Hero
    {
        [Serializable]
        public sealed class HeroModel_View
        {
            public AnimatorStateMachine<AnimatorStateType> AnimMachine = new();
            
            [Section]
            [SerializeField]
            public HeroAnimation_View AnimationView = new();
        
            [Section]
            [SerializeField]
            public HP_View HpView = new();

            [Section] 
            [SerializeField] 
            public Ammo_View AmmoView = new();

            [Serializable]
            public sealed class HeroAnimation_View
            {
                private static readonly int State = Animator.StringToHash("State");
                
                private const int IDLE_STATE = 0;
                private const int MOVE_STATE_FRONT = 1;
                private const int MOVE_STATE_RIGHT = 2;
                private const int MOVE_STATE_LEFT = 3;
                private const int MOVE_STATE_BACK = 4;
                private const int DEATH_STATE = 6;
            
                [SerializeField]
                public Animator animator;
                
                [Construct]
                public void ConstructStates()
                {
                    AnimMachine.Construct(
                
                        (AnimatorStateType.Idle, null),
                
                        (AnimatorStateType.Run, null),
                
                        (AnimatorStateType.Dead, null)
                    );
                }
                
                
                [Construct]
                public void Construct(HeroModel_Core core)
                {
                    var isDeath = core.LifeSectionComp.IsDead;
                    var moveRequired = core.MoveComp.MoveRequired;
                    var inputVector = core.MoveComp.OnMove;
                    
                    isDeath.Subscribe((data) => animator.SetInteger(State, DEATH_STATE));
                    moveRequired.Subscribe((state)=> animator.SetInteger(State, IDLE_STATE));
            
                    inputVector.Subscribe(direction =>
                    {
                        if (isDeath.Value)
                            return;
                        
                        if (direction == Vector3.forward)
                        {
                            animator.SetInteger(State, MOVE_STATE_FRONT);
                            return;
                        }
                        if (direction == -Vector3.forward)
                        {
                            animator.SetInteger(State, MOVE_STATE_BACK);
                            return;
                        }
                        if (direction == Vector3.left)
                        {
                            animator.SetInteger(State, MOVE_STATE_LEFT);
                            return;
                        }
                        if (direction  == Vector3.right)
                        {
                            animator.SetInteger(State, MOVE_STATE_RIGHT);
                        }
                    });
                    
                }
            }
        
            [Serializable]
            public sealed class HP_View
            {
                public AtomicVariable<TextMeshProUGUI> TextHp = new ();
                
                private const string Title = "HIT POINTS: ";
                
                [Construct]
                public void Construct(HeroModel_Core core)
                {
                    var hitPoints = core.LifeSectionComp.HitPoints;
                    TextHp.Value.text = Title + hitPoints.Value;
                    hitPoints.Subscribe((newValue) => TextHp.Value.text = Title + newValue);
                }
            }
            
            
            [Serializable]
            public sealed class Ammo_View
            {
                public AtomicVariable<TextMeshProUGUI> TextAmmo = new ();
                
                private const string Title = "BULLETS: ";
                
                [Construct]
                public void Construct(HeroModel_Core core)
                {
                    var hitPoints = core.AmmoComp.AmmoCount;
                    var maxValue = "/" + core.AmmoComp.MaxAmmo.Value;
                    TextAmmo.Value.text = Title + hitPoints.Value + maxValue;
                    hitPoints.Subscribe((newValue) => TextAmmo.Value.text = Title + newValue + maxValue);
                }
            }
        }
    }

    
