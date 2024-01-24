using System;
using System.Collections.Generic;


namespace GamePlay.Custom.Model
{
    [Serializable]
    public class EnemyService
    {
        private List<Entity.Entity> _enemies = new();

        public void AddEnemy(Entity.Entity enemy)
        {
            _enemies.Add(enemy);
        }

        public List<Entity.Entity> GetAllEnemies()
        {
            return _enemies;
        }
        

        // public Entity.Entity GetClosest(Vector3 position)
        // {
        //     Entity.Entity closestEnemy = null;
        //    
        //     var closestDistance = float.MaxValue;
        //
        //     foreach (var enemy in _enemies)
        //     {
        //         if (enemy == null)
        //             continue;
        //         
        //         var distance = Vector3.Distance(enemy.transform.position, position);
        //
        //         if (!(distance < closestDistance))
        //             continue;
        //         
        //         closestDistance = distance;
        //         closestEnemy = enemy;
        //     }
        //
        //     return closestEnemy;
        // }
        
    }
}