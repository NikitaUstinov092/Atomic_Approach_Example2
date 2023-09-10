using System.Collections;
using System.Declarative.Scripts;
using System.Declarative.Scripts.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace System.Atomic.Implementations
{
    [Serializable]
    public sealed class AtomicTimer : IMonoController, IDisposable
    {
        public event Action OnStarted;
        public event Action OnTimeChanged;
        public event Action OnCompleted;
        public event Action OnStopped;
        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying
        {
            get { return this.isPlaying; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get { return this.currentTime / this.duration; }
            set { this.SetProgress(value); }
        }

        public float Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.currentTime = Mathf.Clamp(value, 0, this.duration); }
        }

        MonoBehaviour IMonoController.MonoContext
        {
            set { this.context = value; }
        }

        [Space, SerializeField]
        private float duration;

        private bool isPlaying;
        private float currentTime;
        private Coroutine coroutine;

        private MonoBehaviour context;

        public AtomicTimer()
        {
        }

        public AtomicTimer(float duration)
        {
            this.duration = duration;
            this.currentTime = 0;
        }

        [Button]
        public void Play()
        {
            if (this.isPlaying)
            {
                return;
            }

            this.isPlaying = true;
            this.OnStarted?.Invoke();
            this.coroutine = this.context.StartCoroutine(this.TimerRoutine());
        }

        [Button]
        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.context.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }

            if (this.isPlaying)
            {
                this.isPlaying = false;
                this.OnStopped?.Invoke();
            }
        }

        [Button]
        public void ResetTime()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }

        

        [Button]
        public void Replay()
        {
            this.ResetTime();
            this.Play();
        }
        
        [Button]
        public void ForcePlay()
        {
            this.Stop();
            this.ResetTime();
            this.Play();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnTimeChanged?.Invoke();
        }

        private IEnumerator TimerRoutine()
        {
            while (this.currentTime < this.duration)
            {
                yield return null;
                this.currentTime += Time.deltaTime;
                this.OnTimeChanged?.Invoke();
            }

            this.isPlaying = false;
            this.coroutine = null;
            this.OnCompleted?.Invoke();
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnStarted);
            DelegateUtils.Dispose(ref this.OnCompleted);
            DelegateUtils.Dispose(ref this.OnTimeChanged);
            DelegateUtils.Dispose(ref this.OnStopped);
            DelegateUtils.Dispose(ref this.OnReset);
        }
    }
}