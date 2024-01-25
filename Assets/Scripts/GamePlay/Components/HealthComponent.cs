using System.Atomic.Implementations;

public class HealthComponent: IGetHealth
{
    private readonly AtomicVariable<int> _health;
    public HealthComponent(AtomicVariable<int> health)
    {
        _health = health;
    }

    AtomicVariable<int> IGetHealth.GetHealth()
    {
        return _health;
    }
    
}
