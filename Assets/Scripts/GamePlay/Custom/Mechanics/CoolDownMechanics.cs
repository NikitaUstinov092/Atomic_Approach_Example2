using System.Atomic.Implementations;
using System.Declarative.Scripts;


namespace GamePlay.Custom.Engines
{
    public class CoolDownMechanics: IUpdate
    {
        private AtomicVariable<float> _coolDownDelay;
        private AtomicVariable<bool> _state;
        private AtomicEvent _callBack ;
        private float _timer;
        
        public void Construct(AtomicVariable<float> coolDownDelay, AtomicVariable<bool> state, AtomicEvent triggerEvent, AtomicEvent callBack)
        {
            _coolDownDelay = coolDownDelay;
            _state = state;
            
            triggerEvent.Subscribe(CoolDown);
            _callBack = callBack;
        }

        void IUpdate.Update(float deltaTime)
        {
            if(_state.Value)
                return;
            
            _timer -= deltaTime;

            if (_timer > 0)
            {
                _state.Value = false;
                return;
            }
            _state.Value = true;
            _callBack?.Invoke();
        }

        private void CoolDown()
        {
            _state.Value = false;
            _timer = _coolDownDelay.Value;
        }
    }
}