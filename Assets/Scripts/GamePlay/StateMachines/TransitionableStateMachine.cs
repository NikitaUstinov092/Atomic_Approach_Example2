using System;
using System.Collections.Generic;
using System.Declarative.Scripts;

namespace GamePlay.StateMachines
{
    public class TransitionableStateMachine<T> : StateMachine<T>, IUpdate
    {
        public delegate bool Predicate();

        private List<(T, Predicate)> transitions = new();
    
        public void AddTransition(T key, Predicate predicate)
        {
            transitions.Add(new ValueTuple<T, Predicate>(key, predicate));
        }

        void IUpdate.Update(float deltaTime)
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
