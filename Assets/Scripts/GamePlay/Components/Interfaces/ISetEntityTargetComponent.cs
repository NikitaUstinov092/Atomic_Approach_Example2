using System.Atomic.Implementations;

namespace GamePlay.Components.Interfaces
{
    public interface ISetEntityTargetComponent
    {
        void SetEntityTarget(AtomicVariable<Entity.Entity> entity);
    }
}
