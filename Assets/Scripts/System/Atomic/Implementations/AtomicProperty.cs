using System.Atomic.Interfaces;

namespace System.Atomic.Implementations
{
    public sealed class AtomicProperty<T> : IAtomicVariable<T>
    {
        public T Value
        {
            get { return this.getter.Invoke(); }
            set { this.setter.Invoke(value); }
        }

        private readonly Func<T> getter;
        private readonly Action<T> setter;

        public AtomicProperty(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}