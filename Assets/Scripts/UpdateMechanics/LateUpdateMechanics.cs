using System;
using System.Declarative.Scripts;

namespace UpdateMechanics
{
    public sealed class LateUpdateMechanics : ILateUpdate
    {
        private Action<float> action;

        public void Construct(Action<float> action)
        {
            this.action = action;
        }

        void ILateUpdate.LateUpdate(float deltaTime)
        {
            this.action.Invoke(deltaTime);
        }

        public void SetAction(Action<object> action1)
        {
            throw new NotImplementedException();
        }
    }
}