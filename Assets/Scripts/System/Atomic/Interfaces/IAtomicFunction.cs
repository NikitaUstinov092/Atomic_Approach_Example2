namespace System.Atomic.Interfaces
{
    public interface IAtomicFunction<in T, out R>
    {
        R Invoke(T args);
    }
}