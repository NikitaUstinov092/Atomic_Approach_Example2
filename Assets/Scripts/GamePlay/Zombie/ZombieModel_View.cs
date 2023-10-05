using System;
using System.Declarative.Scripts.Attributes;
using UnityEngine;

namespace GamePlay.Zombie
{
    [Serializable]
    public class ZombieModel_View
    {
        [Section] 
        [SerializeField] 
        public AnimatorStateMachine<AnimatorStateType> AnimMachine = new();
        
        [SerializeField]
        public Animator Animator;
        

        [Construct]
        public void ConstructStates()
        {
            AnimMachine.Construct(  
                (AnimatorStateType.Idle, null),

                (AnimatorStateType.Run, null), 
                
                (AnimatorStateType.Attack, null),

                (AnimatorStateType.Dead, null)
            );
        }

        [Construct]
        public void ConstructTransitions(ZombieModel_Core.ZombieStates states)
        {
            var coreFSM = states.stateMachine;

            AnimMachine.AddTransition(AnimatorStateType.Idle, () =>
                coreFSM.CurrentState == EnemyStatesType.Idle);

            AnimMachine.AddTransition(AnimatorStateType.Run,
                () => coreFSM.CurrentState == EnemyStatesType.Chase);
            
            AnimMachine.AddTransition(AnimatorStateType.Attack,
                () => coreFSM.CurrentState == EnemyStatesType.Attack);

            AnimMachine.AddTransition(AnimatorStateType.Dead,
                () => coreFSM.CurrentState == EnemyStatesType.Death);
        
        }
    }
}

