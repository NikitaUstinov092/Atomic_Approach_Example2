using System.Atomic.Interfaces;

namespace System.Atomic.Implementations
{
    public sealed class AtomicObservable : IAtomicObservable
    {
        private readonly Action<Action> subscribe;
        private readonly Action<Action> unsubscribe;

        public AtomicObservable(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe(Action action)
        {
            this.subscribe.Invoke(action);
        }

        public void Unsubscribe(Action action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
}