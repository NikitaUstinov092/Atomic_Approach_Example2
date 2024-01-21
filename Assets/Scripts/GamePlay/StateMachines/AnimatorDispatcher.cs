using System;
using System.Atomic.Implementations;
using JetBrains.Annotations;
using UnityEngine;

namespace GamePlay.StateMachines
{
    public sealed class AnimatorDispatcher : MonoBehaviour
    {
        internal event Action<string> OnMessageReceived;
        public AtomicEvent<string> StringReceived;

        [UsedImplicitly]
        private void ReceiveString(string message)
        {
            OnMessageReceived?.Invoke(message);
            StringReceived?.Invoke(message);
        }
    }
}
