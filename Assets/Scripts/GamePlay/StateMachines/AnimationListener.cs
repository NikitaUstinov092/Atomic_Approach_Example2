using System;
using System.Atomic.Implementations;
using UnityEngine;

namespace GamePlay.StateMachines
{
    [Serializable]
    public sealed class AnimationListener
    {
        private readonly AtomicEvent<string> _receivedStringEvent;
        private readonly AtomicEvent _pullTriggeredEvent;
        private readonly string _eventName;
      
        public AnimationListener(AtomicEvent<string> receivedStringEvent, AtomicEvent pullTriggeredEvent, string eventName)
        {
            _receivedStringEvent = receivedStringEvent;
            _pullTriggeredEvent = pullTriggeredEvent;
            _eventName = eventName;
            _receivedStringEvent.Subscribe(OnAnimationEvent);
        }
        private void OnAnimationEvent(string animEvent)
        {
            if (animEvent == _eventName)
            {
                _pullTriggeredEvent.Invoke();
            }
        }
    }
}
