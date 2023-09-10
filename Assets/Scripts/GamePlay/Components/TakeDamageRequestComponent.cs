using System.Atomic.Interfaces;
using GamePlay.Components.Interfaces;

namespace GamePlay.Components
{
    public sealed class TakeDamageRequestComponent : ITakeDamagable
    {
        private readonly IAtomicAction<int> onTakeDamage;

        public TakeDamageRequestComponent(IAtomicAction<int> onTakeDamage)
        {
            this.onTakeDamage = onTakeDamage;
        }

        void ITakeDamagable.TakeDamage(int damage)
        {
            this.onTakeDamage.Invoke(damage);
        }
    }
}
