using System;
using System.Atomic.Interfaces;
using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using Zenject;

namespace GamePlay.Custom.Model
{
    public class KillsCounter: IInitListener, IStartListener, IDisableListener, IKillCounterPM 
    {
        public event Action<int> OnValueChanged;
    
        [Inject]
        private EnemyFactory _enemyFactory;
    
        private Entity.Entity _enemy;
        private int _deathCount;
        void IInitListener.OnInit()
        {
            _enemyFactory.OnEntityCreated += CheckLifeComponent;
        }
        void IStartListener.StartGame()
        {
            OnValueChanged?.Invoke(0);
        }
        void IDisableListener.Disable()
        {
            _enemyFactory.OnEntityCreated -= CheckLifeComponent;
        }
        private void CheckLifeComponent(Entity.Entity enemy)
        {
            if (enemy.TryGet(out IGetDeathEventComponent getDeathEventComponent))
            {
                AddListenerDeathCount(getDeathEventComponent.GetDeathEvent());
                return;
            }
            Debug.LogError($"Компонент LifeSection не найден на сущности {enemy.name}");
        }
        private void AddListenerDeathCount(IAtomicObservable onDeath)
        {
            onDeath.Subscribe(CountDeath);
        }
        private void CountDeath()
        {
            OnValueChanged?.Invoke(++_deathCount);
        }
    }

    public interface IKillCounterPM
    {
        event Action<int> OnValueChanged;
    }
}