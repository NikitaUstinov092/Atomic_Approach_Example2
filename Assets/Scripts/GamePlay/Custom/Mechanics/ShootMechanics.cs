using System.Atomic.Implementations;
using System.Atomic.Interfaces;
using System.Declarative.Scripts;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    public class ShootMechanics: IUpdate
    {
        private AtomicEvent _fireRequest;
        private AtomicEvent _shootApplied;
        private AtomicEvent _onShoot;
        
        private AtomicVariable<int> _ammoCount;
        private AtomicVariable<bool> _death;
        private AtomicVariable<bool> _weaponCooled;
        private AtomicVariable<bool> _canShoot;
        
        private IAtomicAction _shootEngine;
        
        public void Construct(IAtomicAction shootEngine, AtomicEvent fireRequest,
            AtomicEvent shootApplied, AtomicEvent onShoot, AtomicVariable<int> ammoCount, AtomicVariable<bool> death, AtomicVariable<bool> weaponCooled, AtomicVariable<bool> canShoot)
        {
            _shootEngine = shootEngine;
            _fireRequest = fireRequest;
            _shootApplied = shootApplied;
            _onShoot = onShoot;
            
            _ammoCount = ammoCount;
            _death = death;
            _weaponCooled = weaponCooled;
            _canShoot = canShoot;
            
            _fireRequest.Subscribe(RequestFire);
            _onShoot.Subscribe(Shoot);
        }
        void IUpdate.Update(float deltaTime)
        {
            if (_ammoCount.Value <= 0)
            {
                _canShoot.Value = false;
                return;
            }

            if (_death.Value)
            {
                _canShoot.Value = false;
                return;
            }
            
            if (!_weaponCooled.Value)
            {
                _canShoot.Value = false;
                return;
            }
            
            _canShoot.Value = true;
        }
        private void RequestFire()
        {
            if (!_canShoot.Value) 
                return;
            
            _shootApplied?.Invoke();
           
        }
        private void Shoot()
        {
           _shootEngine.Invoke();
        }
    }
}