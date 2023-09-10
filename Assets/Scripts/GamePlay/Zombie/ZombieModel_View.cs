using System;
using System.Declarative.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMechanics;

namespace GamePlay.Zombie
{
    [Serializable]
    public class ZombieModel_View
    {
        private static readonly int State = Animator.StringToHash("State");
        
        private const int MOVE_STATE = 1;
        private const int IDLE_STATE = 2;
        private const int ATTACK_STATE = 3;
        private const int DEATH_STATE = 4;
        
        [SerializeField]
        public Animator Animator;
        
        private readonly LateUpdateMechanics _lateUpdate = new();

        [Construct]
        public void Construct(ZombieModel_Core core)
        {
            var isDeath = core.lifeSection.IsDead;
            var isChasing = core.ZombieChase.IsChasing;
            var stopAttack = core.AttackHero.StopAttack;
            var distanceChecker = core.target;
            
            isDeath.Subscribe((state)=>  Animator.SetInteger(State, DEATH_STATE));
            
            _lateUpdate.Construct(_ =>
            {
                if (isDeath.Value)
                    return;

                if (stopAttack.Value || !distanceChecker.Target.Value)
                {
                    Animator.SetInteger(State, IDLE_STATE);
                    return;
                }
                
                switch (isChasing.Value)
                {
                    case true:
                        Animator.SetInteger(State, MOVE_STATE);
                        break;
                    
                    case false:
                        Animator.SetInteger(State, ATTACK_STATE);
                        break;
                }
            });
        }
    }
}

