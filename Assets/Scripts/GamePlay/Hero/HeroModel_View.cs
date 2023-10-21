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
        public Animation_View Animation = new();
        
        [Section] 
        [SerializeField] 
        public VFX_View VFX = new();
        
        [Section] 
        [SerializeField] 
        public Sound_View Sound = new();
        
        [Section] 
        [SerializeField] 
        public HP_View Hp = new();
        
        [Section] 
        [SerializeField] 
        public Ammo_View Ammo = new();
        
        private static readonly int SHOOT_TRIGGER = Animator.StringToHash("Shoot");
        
        [Serializable]
        public class Animation_View
        {
            [Section]
            public HeroAnimatorStateMachine<AnimatorStateType> AnimMachine = new();
            
            private AnimationListener _animationListener;
        
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
                
                ConstructTriggeredEvents(shoot);
            }

            public void ConstructTriggeredEvents(HeroModel_Core.Shoot shoot)
            {
                _animationListener = new AnimationListener(AnimMachine.Dispatcher.OnStringReceived, 
                    shoot.ShootController.OnShoot, "Shoot");
               
                shoot.ShootController.OnShootApplied.Subscribe(() => AnimMachine.PullTrigger(SHOOT_TRIGGER));
            }
        }

        [Serializable]
        public class VFX_View
        {
            [SerializeField]
            private ParticleSystem _shootEffect;

            private AtomicVariable<Transform> _spawnPointShoot;
            
            [Construct]
            public void Construct(HeroModel_Core.Shoot shoot)
            {
                shoot.ShootController.OnShoot.Subscribe(OnShoot);
                _spawnPointShoot = shoot.SpawnPointShoot;
            }

            private void OnShoot()
            {
                _shootEffect.transform.position = _spawnPointShoot.Value.position;
                _shootEffect.Play();
            }
        }

        [Serializable]
        public class Sound_View
        {
            [SerializeField] 
            private AudioSource _audioSource;
            
            [SerializeField] 
            private AudioClip _shootClip;

            [Construct]
            public void Construct(HeroModel_Core.Shoot shoot)
            {
                shoot.ShootController.OnShoot.Subscribe(()=>_audioSource.PlayOneShot(_shootClip));
            }
            
        }
        
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
            private const string Slash = "/";
            
            [SerializeField] 
            private string Title = "BULLETS: ";

            [Construct]
            public void Construct(HeroModel_Core.Ammo ammoComp)
            {
                var hitPoints = ammoComp.AmmoCount;
                var maxValue = Slash + ammoComp.MaxAmmo.Value;
                TextAmmo.Value.text = Title + hitPoints.Value + maxValue;
                hitPoints.Subscribe((newValue) => TextAmmo.Value.text = Title + newValue + maxValue);
            }
        }
        
       
        
    }

    
}
    

    
