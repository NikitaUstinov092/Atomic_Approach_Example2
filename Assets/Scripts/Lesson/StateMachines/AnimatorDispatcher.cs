using System;
using System.Atomic.Implementations;
using JetBrains.Annotations;
using UnityEngine;


public sealed class AnimatorDispatcher : MonoBehaviour
    {
        internal event Action<string> OnMessageReceived;
        public AtomicEvent<string> OnStringReceived;

        [UsedImplicitly]
        //Called from animator: don't remove!
        public void ReceiveString(string message)
        {
            this.OnMessageReceived?.Invoke(message);
            OnStringReceived?.Invoke(message);
        }
    }
