namespace System.Atomic.Interfaces
{
    public interface IAtomicValue<out T>
    {
        T Value { get; }
    }
}