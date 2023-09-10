using System.Atomic.Interfaces;
using System.Declarative.Scripts.Utils;
using Sirenix.OdinInspector;

namespace System.Atomic.Implementations
{
    [Serializable]
    public class AtomicEvent : IAtomicEvent, IDisposable
    {
        private System.Action onEvent;

        public void Subscribe(System.Action action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(System.Action action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke()
        {
            this.onEvent?.Invoke();
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }

    [Serializable]
    public class AtomicEvent<T> : IAtomicEvent<T>, IDisposable
    {
        private System.Action<T> onEvent;
        
        public void Subscribe(System.Action<T> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(System.Action<T> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T direction)
        {
            this.onEvent?.Invoke(direction);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2> : IAtomicEvent<T1, T2>, IDisposable
    {
        private System.Action<T1, T2> onEvent;
        
        public void Subscribe(System.Action<T1, T2> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(System.Action<T1, T2> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T1 args1, T2 args2)
        {
            this.onEvent?.Invoke(args1, args2);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
    public class AtomicEvent<T1, T2, T3> : IAtomicEvent<T1, T2, T3>, IDisposable
    {
        private System.Action<T1, T2, T3> onEvent;
        
        public void Subscribe(System.Action<T1, T2, T3> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(System.Action<T1, T2, T3> action)
        {
            this.onEvent -= action;
        }

        [Button]
        public virtual void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.onEvent?.Invoke(args1, args2, args3);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
}