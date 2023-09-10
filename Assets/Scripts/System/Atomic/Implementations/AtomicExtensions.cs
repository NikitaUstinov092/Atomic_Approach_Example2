namespace System.Atomic.Implementations
{
    public static class AtomicExtensions
    {
        public static AtomicFunction<T> ToFunction<T>(this T it)
        {
            return new AtomicFunction<T>(() => it);
        }

        public static AtomicAction<T> ToAction<T>(this Action<T> action)
        {
            return new AtomicAction<T>(action);
        }

        public static AtomicVariable<T> ToVariable<T>(this T it)
        {
            return new AtomicVariable<T>(it);
        }
    }
}