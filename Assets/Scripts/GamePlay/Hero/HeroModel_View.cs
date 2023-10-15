using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using Lessons.StateMachines.States;
using TMPro;
using UnityEngine;

namespace GamePlay.Hero
{
    [Serializable]
    public sealed class HeroModel_View
    {
        [Section] 
        [SerializeField] 
        public AnimatorStateMachine<AnimatorStateType> AnimMachine = new();

        [Section] 
        [SerializeField] public HP_View HpView = new();

        [Section] 
        [SerializeField] public Ammo_View AmmoView = new();

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
        public void ConstructTransitions(HeroModel_Core.CharacterStates states)
        {
            var coreFSM = states.StateMachine;

            AnimMachine.AddTransition(AnimatorStateType.Idle, () =>
                coreFSM.CurrentState == CharacterStateType.Idle);

            AnimMachine.AddTransition(AnimatorStateType.Run,
                () => coreFSM.CurrentState == CharacterStateType.Move);

            AnimMachine.AddTransition(AnimatorStateType.Dead,
                () => coreFSM.CurrentState == CharacterStateType.Death);

            AnimMachine.AddTransition(AnimatorStateType.Attack,
                () => coreFSM.CurrentState == CharacterStateType.StandShoot);
        }


        [Serializable]
        public sealed class HP_View
        {
            public AtomicVariable<TextMeshProUGUI> TextHp = new();

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
            public AtomicVariable<TextMeshProUGUI> TextAmmo = new();

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
    

    
