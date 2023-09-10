using System.Atomic.Interfaces;
using System.Declarative.Scripts.Utils;
using Sirenix.OdinInspector;

namespace System.Atomic.Implementations
{
    [Serializable]
    public sealed class AtomicProcess : IAtomicProcess, IDisposable
    {
        public event Action OnStarted;
        public event Action OnStopped;

        public IAtomicValue<bool> Condition { get; set; }

        [ShowInInspector, ReadOnly]
        public bool IsPlaying
        {
            get { return this.isPlaying; }
        }
        
        private bool isPlaying;

        [Title("Methods")]
        [Button]
        public bool CanStart()
        {
            if (this.isPlaying)
            {
                return false;
            }

            if (this.Condition == null)
            {
                return true;
            }

            return this.Condition.Value;
        }

        [Button]
        public void Start()
        {
            if (!this.CanStart())
            {
                return;
            }

            this.isPlaying = true;
            this.OnStarted?.Invoke();
        }

        [Button]
        public void Stop()
        {
            if (!this.isPlaying)
            {
                return;
            }

            this.isPlaying = false;
            this.OnStopped?.Invoke();
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnStarted);
            DelegateUtils.Dispose(ref this.OnStopped);
        }
    }

    [Serializable]
    public sealed class AtomicProcess<T> : IAtomicProcess<T>
    {
        public event Action<T> OnStarted;
        public event Action<T> OnStopped;

        public Func<T, bool> Condition { get; set; }

        [ShowInInspector, ReadOnly]
        public bool IsPlaying
        {
            get { return this.isPlaying; }
        }

        [ShowInInspector, ReadOnly]
        public T State
        {
            get { return this.state; }
        }
        
        private bool isPlaying;
        private T state;

        [Title("Methods")]
        [Button]
        public bool CanStart(T state)
        {
            if (this.isPlaying)
            {
                return false;
            }

            if (this.Condition == null)
            {
                return true;
            }

            return this.Condition.Invoke(state);
        }

        [Button]
        public void Start(T state)
        {
            if (!this.CanStart(state))
            {
                return;
            }

            this.isPlaying = true;
            this.state = state;
            this.OnStarted?.Invoke(state);
        }

        [Button]
        public void Stop()
        {
            if (!this.isPlaying)
            {
                return;
            }

            this.isPlaying = false;

            var state = this.state;
            this.state = default;
            this.OnStopped?.Invoke(state);
        }
        
        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnStarted);
            DelegateUtils.Dispose(ref this.OnStopped);
        }
    }
}