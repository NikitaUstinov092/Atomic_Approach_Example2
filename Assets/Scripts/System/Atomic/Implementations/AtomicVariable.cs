using System.Atomic.Interfaces;
using System.Declarative.Scripts.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace System.Atomic.Implementations
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>, IDisposable
    {
        public T Value
        {
            get => value;
            set => SetValue(value);
        }

        public void Subscribe(Action<T> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            this.onChanged -= listener;
        }

        public Action<T> onChanged;

        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }
        public AtomicVariable(T value)
        {
            this.value = value;
        }
        
        protected virtual void SetValue(T value)
        {
            this.value = value;
            onChanged?.Invoke(value);
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            this.onChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}