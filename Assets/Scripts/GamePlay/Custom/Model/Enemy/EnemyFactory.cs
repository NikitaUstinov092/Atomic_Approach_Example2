using System;
using GamePlay.Components.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;


namespace GamePlay.Custom.Model
{
    public class EnemyFactory 
    {
        public event Action<Entity.Entity> OnEntityCreated;
        
        public void CreateEnemy(Entity.Entity enemy, Entity.Entity target, Vector3 spawnPosition, Transform parent = null)
        {
            var spawnEntity =  Object.Instantiate(enemy, spawnPosition, Quaternion.identity);
            AddTarget(spawnEntity, target);
            
            if (parent != null)
            {
                spawnEntity.transform.parent = parent.transform;
            }
            
            OnEntityCreated?.Invoke(spawnEntity);
        }
        
        private void AddTarget(Entity.Entity enemy, Entity.Entity target)
        {
            if(enemy.TryGet(out ISetEntityTargetComponent setEntityComp))
                setEntityComp.SetEntityTarget(target);
            else
                throw new NullReferenceException();
        }
    }
}
