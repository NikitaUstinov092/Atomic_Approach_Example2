using System;
using System.Atomic.Implementations;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Components;
using GamePlay.Custom;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using Zenject;

public class ClosestEntitySearcher : MonoBehaviour, IInitListener, IDisableListener
{
    public AtomicEvent <Entity.Entity> OnClosestEntityChanged;
    
    [SerializeField] 
    private Entity.Entity _sourceEntity;
    
    private readonly Dictionary<Entity.Entity, float> _entitiesDistanceData = new();
    
    private IEntityFactory<Entity.Entity> _entityFactory;
    private Entity.Entity _closestEntity;

    [Inject]
    private void Construct(IEntityFactory<Entity.Entity> entityFactory)
    {
        _entityFactory = entityFactory;
    }
    void IInitListener.OnInit()
    {
        _entityFactory.OnEntityCreated += AddEntity;
    }
    void IDisableListener.Disable()
    {
        _entityFactory.OnEntityCreated -= AddEntity;
    }
    public void Update()
    {
        UpdateDistance();
        FindClosest();
    }

    private void UpdateDistance()
    {
        foreach (var t in _entitiesDistanceData.Keys.ToList())
        {
            if (EntityNull(t))
                continue;
            
            var distance = (_sourceEntity.transform.position - t.transform.position).magnitude;
            _entitiesDistanceData[t] = distance;
        }
    }

    private void FindClosest()
    {
        var sortedDictionary = _entitiesDistanceData.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        var firstElement = sortedDictionary.Keys.FirstOrDefault();
        
         if(_closestEntity == firstElement)
             return;
         
         _closestEntity = firstElement;
         OnClosestEntityChanged?.Invoke(_closestEntity);
    }

    private void AddEntity(Entity.Entity entity)
    {
        if (!entity.TryGet(out IGetDeathEventComponent deathEvent)) 
            throw new NullReferenceException($"Компонент DeathEventComponent не найден на сущности {entity}");
      
        deathEvent.GetDeathEventData().Subscribe(RemoveEntity);
       
        _entitiesDistanceData.Add(entity,new float());
    }

    private bool EntityNull(Entity.Entity entity)
    {
        return entity == null;
    }
    
    private void RemoveEntity(Entity.Entity entity)
    {
        _entitiesDistanceData.Remove(entity);
    }
}
