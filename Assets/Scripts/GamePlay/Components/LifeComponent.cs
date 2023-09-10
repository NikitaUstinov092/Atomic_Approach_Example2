using System.Atomic.Implementations;
using GamePlay.Hero;

namespace GamePlay.Components
{
    public interface IGetDeathEventComponent
    {
        AtomicEvent GetDeathEvent();
    }

    public class DeathEventComponent : IGetDeathEventComponent
    {
        private readonly AtomicEvent DeathEvent;
    
        public DeathEventComponent(AtomicEvent deathEvent)
        {
            DeathEvent = deathEvent;
        }
    
        public AtomicEvent GetDeathEvent()
        {
            return DeathEvent;
        }
    }
}