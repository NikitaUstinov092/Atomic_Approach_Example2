namespace System.Atomic.Interfaces
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        new T Value { get; set; }
    }
}