using System;
using System.Collections.Generic;
using GamePlay.Custom.GameMachine;

namespace Lessons.StateMachines
{
    public class TransitionableStateMachine<T> : StateMachine<T>, IUpdateListener
    {
        public delegate bool Predicate();

        private List<(T, Predicate)> transitions = new();
    
        public void AddTransition(T key, Predicate predicate)
        {
            transitions.Add(new ValueTuple<T, Predicate>(key, predicate));
        }

        void IUpdateListener.Update()
        {
            foreach (var (stateType, condition) in transitions)
            {
                if (!stateType.Equals(this.currentStateType) && condition.Invoke())
                {
                    SwitchState(stateType);
                }
            }
        }
    }
}