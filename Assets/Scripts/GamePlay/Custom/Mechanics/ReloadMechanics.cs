using System.Atomic.Implementations;

namespace GamePlay.Custom.Engines
{
    public class ReloadMechanics
    {
        private AtomicEvent _onReloadComplete;
        private AtomicEvent _reloadRequest;
      
        private AtomicVariable<int> _ammoCount;
        private AtomicVariable<int> _maxAmmo;
        
        public void Construct(AtomicVariable<int> ammoCount, AtomicVariable<int> maxAmmo,
            AtomicEvent reloadRequest, AtomicEvent onReloadComplete)
        {
            _ammoCount = ammoCount;
            _maxAmmo = maxAmmo;
         
            _onReloadComplete = onReloadComplete;
            _reloadRequest = reloadRequest;
            
            Init();
        }

        private void Init()
        {
            CheckNeedReload();
            _ammoCount.Subscribe((_) =>
            {
                CheckNeedReload();
            });
            _onReloadComplete.Subscribe(Reload);
        }

        private void Reload()
        {
            if (_ammoCount.Value < _maxAmmo.Value)
            {
                _ammoCount.Value++;
            }
        }
        
        private void CheckNeedReload()
        {
            if (_ammoCount.Value < _maxAmmo.Value)
            {
                _reloadRequest?.Invoke();
            }
        }
    }
}