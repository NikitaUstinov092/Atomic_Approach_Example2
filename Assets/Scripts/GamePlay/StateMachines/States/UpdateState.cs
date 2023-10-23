using System.Declarative.Scripts;

namespace GamePlay.StateMachines.States
{
    public abstract class UpdateState : IState, IUpdate
    {
        private bool _enabled = false;
        
        void IState.Enter()
        {
            _enabled = true;
            OnEnter();
        }

        protected virtual void OnEnter()
        {
            
        }

        void IState.Exit()
        {
            _enabled = false;
            OnExit();
        }

        protected virtual void OnExit()
        {
            
        }

        void IUpdate.Update(float deltaTime)
        {
            if (_enabled)
            {
                OnUpdate(deltaTime);
            }
        }

        protected abstract void OnUpdate(float deltaTime);
       
    }
}