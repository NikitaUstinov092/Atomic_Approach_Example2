using System.Atomic.Interfaces;
using Sirenix.OdinInspector;

namespace System.Atomic.Implementations
{
    [Serializable]
    public sealed class AtomicAction : IAtomicAction
    {
        private System.Action action;

        public AtomicAction()
        {
        }

        public AtomicAction(System.Action action)
        {
            this.action = action;
        }

        public void Use(Action action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke()
        {
            this.action?.Invoke();
        }
    }

    [Serializable]
    public sealed class AtomicAction<T> : IAtomicAction<T>
    {
        private System.Action<T> action;

        public AtomicAction()
        {
        }

        public AtomicAction(System.Action<T> action)
        {
            this.action = action;
        }
        
        public void Use(Action<T> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T direction)
        {
            this.action?.Invoke(direction);
        }
    }
    
    [Serializable]
    public sealed class AtomicAction<T1, T2> : IAtomicAction<T1, T2>
    {
        private System.Action<T1, T2> action;

        public AtomicAction()
        {
        }

        public AtomicAction(System.Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public void Use(Action<T1, T2> action)
        {
            this.action = action;
        }

        [Button]
        public void Invoke(T1 args1, T2 args2)
        {
            this.action?.Invoke(args1, args2);
        }
    }

}