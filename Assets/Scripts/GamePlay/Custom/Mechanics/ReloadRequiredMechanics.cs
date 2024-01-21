using System.Atomic.Implementations;
using System.Declarative.Scripts;

namespace GamePlay.Custom.Engines
{
    public class ReloadRequiredMechanics: IFixedUpdate
    {
        private AtomicVariable<int> _ammoCount;
        private AtomicVariable<int> _maxAmmo;
        private AtomicVariable<bool> _reloadRequired;
        
        public void Construct(AtomicVariable<int> ammoCount, AtomicVariable<int> maxAmmo, AtomicVariable<bool> reloadRequired)
        {
            _ammoCount = ammoCount;
            _maxAmmo = maxAmmo;
            _reloadRequired = reloadRequired;
        }
        
        void IFixedUpdate.FixedUpdate(float deltaTime)
        {
            _reloadRequired.Value = _ammoCount.Value < _maxAmmo.Value;
        }
    }
}