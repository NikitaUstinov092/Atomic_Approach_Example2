using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Components;
using GamePlay.Custom;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ClosestEntitySearcher : MonoBehaviour, IInitListener, IDisableListener, IUpdateListener
{
    public event Action <Entity.Entity> OnClosestEntityChanged;
    
    [Inject]
    private IEntityFactory<Entity.Entity> _entityFactory;

    [SerializeField]
    private Entity.Entity _entitySource;
    
    [SerializeField]
    private Entity.Entity _closestEntity;
    
    private readonly Dictionary<Entity.Entity, float> _entitiesDistanceData = new();

    private float _minDistance;
    
    void IInitListener.OnInit()
    {
        _entityFactory.OnEntityCreated += AddEntity;
    }
    void IDisableListener.Disable()
    {
        _entityFactory.OnEntityCreated -= AddEntity;
    }
    void IUpdateListener.Update()
    {
        if(EntityNull(_entitySource))
            return;
        
        UpdateDistance();
        FindClosest();
    }

    private void UpdateDistance()
    {
        foreach (var t in _entitiesDistanceData.Keys.ToList())
        {
            if (EntityNull(t))
                continue;
            
            var distance = (_entitySource.transform.position - t.transform.position).magnitude;
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
        if (!entity.TryGet(out DeathEventComponent deathEvent)) 
            throw new NullReferenceException();
      
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
