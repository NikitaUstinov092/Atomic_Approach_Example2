using System;
using Elementary;
using Lessons.StateMachines.States;
using UnityEditorInternal;

namespace Game.GameEngine.Animation
{
    public sealed class AnimatorState_ListenEvent : IState
    {
        private AnimatorStateMachine<AnimatorStateType> animationSystem;

        private IAction action;

        private string[] animationEvents;

        public void ConstructAnimMachine(AnimatorStateMachine<AnimatorStateType> machine)
        {
            this.animationSystem = machine;
        }

        public void ConstructAnimEvents(params string[] animEvents)
        {
            this.animationEvents = animEvents;
        }

        public void ConstructAction(IAction action)
        {
            this.action = action;
        }

        public void ConstructAction(Action action)
        {
            this.action = new ActionDelegate(action);
        }

        public void Enter()
        {
            this.animationSystem.OnMessageReceived += this.OnAnimationEvent;
        }

        public void Exit()
        {
            this.animationSystem.OnMessageReceived -= this.OnAnimationEvent;
        }

        private void OnAnimationEvent(string message)
        {
            if (this.ContainsEvent(message))
            {
                this.action.Do();
            }
        }

        private bool ContainsEvent(string message)
        {
            for (int i = 0, count = this.animationEvents.Length; i < count; i++)
            {
                if (this.animationEvents[i] == message)
                {
                    return true;
                }
            }

            return false;
        }
    }
}