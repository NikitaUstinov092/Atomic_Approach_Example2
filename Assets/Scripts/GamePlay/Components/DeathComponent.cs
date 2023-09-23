using System.Atomic.Implementations;

namespace GamePlay.Components
{
    public interface IGetDeathEventComponent
    {
        AtomicEvent GetDeathEvent();
        AtomicEvent <Entity.Entity> GetDeathEventData();
    }

    public class DeathEventComponent : IGetDeathEventComponent
    {
        private readonly AtomicEvent DeathEvent;
        private readonly AtomicEvent<Entity.Entity> DeathEventData;
    
        public DeathEventComponent(AtomicEvent deathEvent, AtomicEvent<Entity.Entity> deathEventData)
        {
            DeathEvent = deathEvent;
            DeathEventData = deathEventData;
        }
    
        public AtomicEvent GetDeathEvent()
        {
            return DeathEvent;
        }
        
        public AtomicEvent<Entity.Entity> GetDeathEventData()
        {
            return DeathEventData;
        }
    }
}