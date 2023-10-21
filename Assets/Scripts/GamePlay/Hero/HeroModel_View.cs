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
        public Animation Animations = new();
        
        [Section] 
        [SerializeField] 
        public VFX Vfx = new();

        [Section] 
        [SerializeField] 
        public HP_View HpView = new();

        [Section] 
        [SerializeField] 
        public Ammo_View AmmoView = new();
        
        private static readonly int SHOOT_TRIGGER = UnityEngine.Animator.StringToHash("Shoot");
        private static readonly int DAMAGE_TRIGGER = UnityEngine.Animator.StringToHash("DAMAGE"); //TO DO
        

        [Serializable]
        public class Animation
        {
            [Section]
            public HeroAnimatorStateMachine<AnimatorStateType> AnimMachine = new();
            
            private CharacterAnimationListener _animationListener;
        
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
            public void ConstructTransitions(HeroModel_Core.CharacterStates states, HeroModel_Core.Shoot shoot)
            {
                var coreFSM = states.StateMachine;

                AnimMachine.AddTransition(AnimatorStateType.Idle, () =>
                    coreFSM.CurrentState == CharacterStateType.Idle);

                AnimMachine.AddTransition(AnimatorStateType.Run,
                    () => coreFSM.CurrentState == CharacterStateType.Move);

                AnimMachine.AddTransition(AnimatorStateType.Dead,
                    () => coreFSM.CurrentState == CharacterStateType.Death);
            
                shoot.ShootController.OnShootApplied.Subscribe(OnShoot);

                _animationListener = new(AnimMachine.Dispatcher,shoot.ShootController.OnShoot, "Shoot");
            }
            private void OnShoot()
            {
                AnimMachine.PullTrigger(SHOOT_TRIGGER);
            }
        }

        [Serializable]
        public class VFX
        {
            [SerializeField]
            private ParticleSystem _shootEffect;

            private AtomicVariable<Transform> _spawnPointShoot;
            
            [Construct]
            public void Construct(HeroModel_Core.Shoot shoot)
            {
                shoot.ShootController.OnShootApplied.Subscribe(OnShoot);
                _spawnPointShoot = shoot.SpawnPointShoot;
            }

            private void OnShoot()
            {
                _shootEffect.transform.position = _spawnPointShoot.Value.position;
                _shootEffect.Play();
            }
        }

        /*private void UpdateBodyLayer()
        {
            var weight = this.isAlive.Value ? 1 : 0;
            this.animator.SetLayerWeight(this.bodyLayer, weight);
        }*/
        
        /*private void OnTakeDamage(TakeDamageArgs args)
        {
            if (args.damage > 0)
            {
                this.animator.SetTrigger(TAKE_DAMAGE_TRIGGER);
            }
        }*/

        [Serializable]
        public sealed class HP_View
        {
            public AtomicVariable<TextMeshProUGUI> TextHp = new();

            [SerializeField] 
            private string Title = "HIT POINTS: ";

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

            [SerializeField] 
            private string Title = "BULLETS: ";

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
    

    
