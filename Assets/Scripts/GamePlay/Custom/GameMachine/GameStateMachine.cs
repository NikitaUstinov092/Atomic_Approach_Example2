using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GamePlay.Custom.GameMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        [Inject]
        private readonly DiContainer _container;
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            StartGame();
        }
        private void Update()
        {
            UpdateGameListener();
        }
        private void OnDisable()
        {
            Disable();
        }
        private void Init()
        {
            foreach (var listener in _container.Resolve<IEnumerable<IInitListener>>())
            {
                listener.OnInit();
            }
        }
        private void StartGame()
        {
            foreach (var listener in _container.Resolve<IEnumerable<IStartListener>>())
            {
                listener.StartGame();
            }
        }
        private void Disable()
        {
            foreach (var listener in _container.Resolve<IEnumerable<IDisableListener>>())
            {
                listener.Disable();
            }
        }
        private void UpdateGameListener()
        {
            foreach (var listener in _container.Resolve<IEnumerable<IUpdateListener>>())
            {
                listener.Update();
            }
        }
    }
}
