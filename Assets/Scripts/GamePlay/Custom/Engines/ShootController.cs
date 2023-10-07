using System;
using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Hero;

namespace GamePlay.Custom.Engines
{
    [Serializable]
    public class ShootController: IFixedUpdate
    {
        public AtomicEvent OnGetPressedFire = new();
        public AtomicEvent OnBulletCreated = new();
        
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
            
            OnGetPressedFire.Subscribe(Fire);
        }
        
        void IFixedUpdate.FixedUpdate(float deltaTime)
        {
            CoolDownTimer(deltaTime);
        }
        private void Fire()
        {
            if(_coolDown || !_canShoot)
                return;
                    
            _coolDown = true;
            _shootEngine.CreateBullet();
            OnBulletCreated?.Invoke();
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