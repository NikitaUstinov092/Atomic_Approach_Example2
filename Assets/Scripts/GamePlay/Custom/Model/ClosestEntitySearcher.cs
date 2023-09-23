using System;
using System.Collections.Generic;
using GamePlay.Components;
using GamePlay.Components.Interfaces;
using GamePlay.Custom;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using Zenject;

public class ClosestEntitySearcher : MonoBehaviour, IInitListener, IDisableListener, IUpdateListener
{
    public event Action <Entity.Entity> OnClosestEntityChanged;
    
    [Inject]
    private IEnemyFactory<Entity.Entity> _enemyFactory;

    [SerializeField]
    private Entity.Entity _entityTarget;
    
    [SerializeField]
    private List<Entity.Entity> _entitiesSearch = new();
    
    [SerializeField]
    private Entity.Entity _closestEntity;
    
    /*private float _closestDistance = float.MaxValue; */
    [SerializeField]
    private Dictionary<Entity.Entity, float> __entitiesDistanceData = new();
    void IInitListener.OnInit()
    {
        _enemyFactory.OnEnemyCreated += AddEntity;
    }
    void IDisableListener.Disable()
    {
        _enemyFactory.OnEnemyCreated -= AddEntity;
    }
    void IUpdateListener.Update()
    {
       
        
    }

    /*
    void IUpdateListener.Update()
    {
        foreach (var t in _entitiesSearch)
        {
            if (t == null)
            {
                _entitiesSearch.Remove(t);
                return;
            }
            
            var distance = (_entityTarget.transform.position - t.transform.position).magnitude;

            if (!(distance < _closestDistance)) 
                continue;
            
            _closestDistance = distance;// Обновляем ближайшую дистанцию
            
            if(t == _closestEntity)
                return;
            
            _closestEntity = t;
            
            OnClosestEntityChanged?.Invoke(_closestEntity);
            Debug.Log($"Новый приследуемый враг {_closestEntity.name}");
        }
    }
    */

    private void AddEntity(Entity.Entity entity)
    {
        if(EntityNull(_entityTarget))
            return;

        if (!entity.TryGet(out DeathEventComponent deathEvent)) 
            throw new NullReferenceException();
      
        deathEvent.GetDeathEventData().Subscribe(RemoveEntity);
        _entitiesSearch.Add(entity);
    }

    private bool EntityNull(Entity.Entity entity)
    {
        return entity == null;
    }
    
    private void RemoveEntity(Entity.Entity entity)
    {
        _entitiesSearch.Remove(entity);
    }
}
