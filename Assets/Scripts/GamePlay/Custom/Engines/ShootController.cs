using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Hero;

namespace GamePlay.Custom.Engines
{
    [Serializable]
    public class ShootController: IFixedUpdate
    {
        public AtomicEvent FireRequest = new();
        public AtomicEvent OnStartShoot = new();
        public AtomicEvent OnEndShoot = new();
        
        private AtomicVariable<float> _coolDownDelay = new();
        private ShootEngine _shootEngine;
        
        private bool _canShoot = true;
        private bool _coolDown;
        private float _timer;
        
        public void Construct(HeroModel_Core.Shoot soot, AtomicVariable<int> ammoCount, AtomicVariable<bool> death)
        {
            _shootEngine = soot.ShootEngine;
            _coolDownDelay = soot.CoolDown;
            
            ammoCount.Subscribe((count) => _canShoot = (count > 0));
            death.Subscribe((data) => _canShoot = !data);
            
            FireRequest.Subscribe(RequestFire);
        }
        
        void IFixedUpdate.FixedUpdate(float deltaTime)
        {
            CoolDownTimer(deltaTime);
        }
        private void RequestFire()
        {
            if(_coolDown || !_canShoot)
                return;
                    
            _coolDown = true;
            
            OnStartShoot?.Invoke();
            Shoot();
            OnEndShoot?.Invoke();
        }

        private void Shoot()
        {
            _shootEngine.CreateBullet();
        }
        private void CoolDownTimer(float deltaTime)
        {
            if(!_canShoot || !_coolDown)
                return;
                        
            _timer += deltaTime;

            if (_timer <= _coolDownDelay.Value 
                || !_canShoot) 
                return;
                        
            _timer = 0;
            _coolDown = false;
        }
    }
}