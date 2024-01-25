using System.Atomic.Implementations;

public interface IGetHealth
{
   AtomicVariable<int> GetHealth();
}
