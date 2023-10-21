using System;
using System.Atomic.Implementations;


    [Serializable]
    public sealed class CharacterAnimationListener
    {
        private readonly AnimatorDispatcher _dispatcher;
        private readonly AtomicEvent _fireEvent;
        private readonly string _eventName;
        public CharacterAnimationListener(AnimatorDispatcher dispatcher, AtomicEvent fireEvent, string eventName)
        {
            _dispatcher = dispatcher;
            _fireEvent = fireEvent;
            _eventName = eventName;
        }
        
        public void OnEnable()
        {
            _dispatcher.OnMessageReceived += OnAnimationEvent;
        }
        public void OnDisable()
        {
            _dispatcher.OnMessageReceived -= OnAnimationEvent;
        }
        private void OnAnimationEvent(string animEvent)
        {
            if (animEvent == _eventName)
            {
                _fireEvent.Invoke();
            }
        }
    }
