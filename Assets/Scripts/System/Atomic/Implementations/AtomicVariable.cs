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
            onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            onChanged -= listener;
        }

        public Action<T> onChanged;

        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T value;

        public AtomicVariable()
        {
            value = default;
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
        
        public static implicit operator T(AtomicVariable<T> value)
        {
            return value.value;
        }

        public static implicit operator AtomicVariable<T>(T value)
        {
            return new AtomicVariable<T>(value);
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            onChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            DelegateUtils.Dispose(ref onChanged);
        }
    }
}