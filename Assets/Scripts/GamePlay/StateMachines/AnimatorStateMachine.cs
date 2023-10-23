using System;
using UnityEngine;

namespace GamePlay.StateMachines
{
    [Serializable]
    public class AnimatorStateMachine<T> : TransitionableStateMachine<T> where T : Enum
    {
        private static readonly int State = Animator.StringToHash("State");

        public event Action<string> OnMessageReceived
        {
            add { Dispatcher.OnMessageReceived += value; }
            remove { Dispatcher.OnMessageReceived -= value; }
        }

        [SerializeField]
        protected Animator animator;

        [SerializeField]
        public AnimatorDispatcher Dispatcher;

        public override void SwitchState(T stateType)
        {
            base.SwitchState(stateType);
            animator.SetInteger(State, Convert.ToInt32(stateType));
        }
    }
}
