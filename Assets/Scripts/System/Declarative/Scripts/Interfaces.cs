using UnityEngine;

namespace System.Declarative.Scripts
{
    public interface IAwake : IMonoElement
    {
        void Awake();
    }

    public interface IEnable : IMonoElement
    {
        void OnEnable();
    }

    public interface IStart : IMonoElement
    {
        void Start();
    }

    public interface IFixedUpdate : IMonoElement
    {
        void FixedUpdate(float deltaTime);
    }

    public interface IUpdate : IMonoElement
    {
        void Update(float deltaTime);
    }

    public interface ILateUpdate : IMonoElement
    {
        void LateUpdate(float deltaTime);
    }

    public interface IDisable : IMonoElement
    {
        void OnDisable();
    }

    public interface IDestroy : IMonoElement
    {
        void OnDestroy();
    }

    public interface IMonoController : IMonoElement
    {
        MonoBehaviour MonoContext { set; }
    }

    public interface IMonoElement
    {
    }
}