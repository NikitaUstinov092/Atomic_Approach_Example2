using System.Atomic.Interfaces;
using Sirenix.OdinInspector;

namespace System.Atomic.Implementations
{
    [Serializable]
    public sealed class AtomicFunction<T> : IAtomicValue<T>
    {
        [ShowInInspector, ReadOnly]
        public T Value
        {
            get
            {
                if (this.func != null)
                {
                    return this.func.Invoke();
                }

                return default;
            }
        }

        private Func<T> func;

        public AtomicFunction(Func<T> func)
        {
            this.func = func;
        }

        public void Use(Func<T> func)
        {
            this.func = func;
        }
    }

    [Serializable]
    public sealed class AtomicFunction<T, R> : IAtomicFunction<T, R>
    {
        private Func<T, R> func;

        public AtomicFunction()
        {
        }

        public AtomicFunction(Func<T, R> func)
        {
            this.func = func;
        }

        public void Use(Func<T, R> func)
        {
            this.func = func;
        }

        [Button]
        public R Invoke(T args)
        {
            return this.func.Invoke(args);
        }
    }
}