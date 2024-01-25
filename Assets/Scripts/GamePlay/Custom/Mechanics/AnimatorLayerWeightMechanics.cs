using System.Atomic.Implementations;
using System.Declarative.Scripts;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    public class AnimatorLayerWeightMechanics: IUpdate
    {
        private AtomicVariable<Animator> _animator;
        private AtomicVariable<bool> _isDead;
        private int _bodyLayer;
        
        public void Construct(AtomicVariable<Animator> animator, AtomicVariable<bool> isAlive, AtomicVariable<string> bodyLayer)
        {
            _animator = animator;
            _isDead = isAlive;
            _bodyLayer = _animator.Value.GetLayerIndex(bodyLayer.Value);
        }
        
        void IUpdate.Update(float deltaTime)
        {
            var weight = _isDead.Value ? 0 : 1;
            _animator.Value.SetLayerWeight(_bodyLayer, weight);
        }
    }
}