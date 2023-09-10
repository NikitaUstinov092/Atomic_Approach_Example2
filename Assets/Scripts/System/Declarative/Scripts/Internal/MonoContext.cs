using System.Collections.Generic;
using UnityEngine;

namespace System.Declarative.Scripts.Internal
{
    internal sealed class MonoContext
    {
        private readonly MonoBehaviour root;
        
        private readonly List<IAwake> awakeComponents = new();
        private readonly List<IEnable> enableComponents = new();
        private readonly List<IStart> startComponents = new();
        private readonly List<IFixedUpdate> fixedUpdateComponents = new();
        private readonly List<IUpdate> updateComponents = new();
        private readonly List<ILateUpdate> lateUpdateComponents = new();
        private readonly List<IDisable> disableComponents = new();
        private readonly List<IDestroy> destroyComponents = new();

        public MonoContext(MonoBehaviour root)
        {
            this.root = root;
        }

        internal void AddListener(IMonoElement element)
        {
            if (element is IMonoController injectiveComponent)
            {
                injectiveComponent.MonoContext = this.root;
            }
            
            if (element is IAwake awakeComponent)
            {
                this.awakeComponents.Add(awakeComponent);
            }

            if (element is IEnable enableComponent)
            {
                this.enableComponents.Add(enableComponent);
            }

            if (element is IStart startComponent)
            {
                this.startComponents.Add(startComponent);
            }

            if (element is IFixedUpdate fixedUpdateComponent)
            {
                this.fixedUpdateComponents.Add(fixedUpdateComponent);
            }

            if (element is IUpdate updateComponent)
            {
                this.updateComponents.Add(updateComponent);
            }

            if (element is ILateUpdate lateUpdateComponent)
            {
                this.lateUpdateComponents.Add(lateUpdateComponent);
            }

            if (element is IDisable disableComponent)
            {
                this.disableComponents.Add(disableComponent);
            }
        }

        public void Awake()
        {
            for (int i = 0, count = this.awakeComponents.Count; i < count; i++)
            {
                var listener = this.awakeComponents[i];
                listener.Awake();
            }
        }

        public void OnEnable()
        {
            for (int i = 0, count = this.enableComponents.Count; i < count; i++)
            {
                var listener = this.enableComponents[i];
                listener.OnEnable();
            }
        }

        public void Start()
        {
            for (int i = 0, count = this.startComponents.Count; i < count; i++)
            {
                var listener = this.startComponents[i];
                listener.Start();
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            for (int i = 0, count = this.fixedUpdateComponents.Count; i < count; i++)
            {
                var listener = this.fixedUpdateComponents[i];
                listener.FixedUpdate(deltaTime);
            }
        }

        public void Update(float deltaTime)
        {
            for (int i = 0, count = this.updateComponents.Count; i < count; i++)
            {
                var listener = this.updateComponents[i];
                listener.Update(deltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            for (int i = 0, count = this.lateUpdateComponents.Count; i < count; i++)
            {
                var listener = this.lateUpdateComponents[i];
                listener.LateUpdate(deltaTime);
            }
        }

        public void OnDisable()
        {
            for (int i = 0, count = this.disableComponents.Count; i < count; i++)
            {
                var listener = this.disableComponents[i];
                listener.OnDisable();
            }
        }

        public void OnDestroy()
        {
            for (int i = 0, count = this.destroyComponents.Count; i < count; i++)
            {
                var listener = this.destroyComponents[i];
                listener.OnDestroy();
            }
        }
    }
}