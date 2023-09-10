using System;
using System.Declarative.Scripts;

namespace UpdateMechanics
{
    public sealed class FixedUpdateMechanics : IFixedUpdate
    {
        private Action<float> action;

        public void Construct(Action<float> action)
        {
            this.action = action;
        }

        void IFixedUpdate.FixedUpdate(float deltaTime)
        {
            this.action.Invoke(deltaTime);
        }
    }
}
